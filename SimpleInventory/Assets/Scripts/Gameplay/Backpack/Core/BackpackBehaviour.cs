using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Core.Modules;
using Gameplay.Backpack.Core.States;
using Gameplay.Extensions;
using Gameplay.Items;
using Gameplay.Section;
using Gameplay.Services.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Backpack.Core
{
    public sealed class BackpackBehaviour : MonoBehaviour
    {
        public UnityEvent<ItemType, BackpackSectionType> OnStoredEvent => _onStoredEvent;
        public UnityEvent<ItemType, BackpackSectionType> OnClearedEvent => _onClearedEvent;

        [SerializeField] private UnityEvent<ItemType, BackpackSectionType> _onStoredEvent;
        [SerializeField] private UnityEvent<ItemType, BackpackSectionType> _onClearedEvent;
        [SerializeField] private Transform _storeVfxPoint;
        [SerializeField] private ParticleSystem _storeVfxPrefab;
        [SerializeField] private SectionBehaviour[] _sectionBehaviours;
        [SerializeField] private StorableTriggerObserver _storableTriggerObserver;

        private IStateMachine<BaseBackpackState> _stateMachine;
        private ItemsStorageService _storageService;
        private IBackpackContext _context;

        private readonly List<IStorable> _itemsWaitingForCleaning = new();
        private readonly List<IStorable> _itemsWaitingForStoring = new();

        private WaitForItemState _waitForItemState;
        private StoreItemState _storeItemState;
        private ClearSectionState _clearSectionState;
        public IBackpackContext Context => _context;

        public async Task SetRuntime(BackpackRuntimeData runtimeData)
        {
            await RunInitialState(runtimeData);
            await _stateMachine.ChangeStateAsync(_waitForItemState, destroyCancellationToken, true);
        }

        private async Task RunInitialState(BackpackRuntimeData runtimeData)
        {
            _stateMachine.Run();

            var initializeData = new InitializeBackpackData(_sectionBehaviours, runtimeData, _context);
            var initializeState = new InitializeBackpackState(initializeData);

            await _stateMachine.ChangeStateAsync(initializeState, destroyCancellationToken, true);
        }

        private void Awake()
        {
            InitializeInternal();
            InitializeStates();
            InitializeModules();
        }

        private void InitializeModules()
        {
            var loggerModule = new LoggerModule<BaseBackpackState>();
            _stateMachine.AddModule(loggerModule);
        }

        private void InitializeInternal()
        {
            _stateMachine = new StateMachine<BaseBackpackState>();
            _context = new BackpackContext(_sectionBehaviours);
        }

        private void InitializeStates()
        {
            _waitForItemState = new WaitForItemState();
            _clearSectionState = new ClearSectionState(_itemsWaitingForCleaning, _context);
            var storeData = new StoreItemData(_storeVfxPrefab, _storeVfxPoint, _context, _itemsWaitingForStoring);
            _storeItemState = new StoreItemState(storeData);
        }

        private void Start()
        {
            _storageService = ServiceLocator.Get<ItemsStorageService>();
            _storableTriggerObserver.OnEntered += OnStorableEntered;
            _storageService.OnSectionCleared += OnSectionDataCleared;
            _context.OnStored += OnItemStored;
            _context.OnCleared += OnSectionCleared;
        }

        private void OnDestroy()
        {
            _storableTriggerObserver.OnEntered -= OnStorableEntered;
            _storageService.OnSectionCleared -= OnSectionDataCleared;
            _context.OnStored -= OnItemStored;
            _context.OnCleared -= OnSectionCleared;
        }

        private void OnSectionCleared(ItemType item, BackpackSectionType section)
        {
            _onClearedEvent.Invoke(item, section);
        }

        private void OnItemStored(ItemType item, BackpackSectionType section)
        {
            _onStoredEvent.Invoke(item, section);
        }

        private async void OnStorableEntered(IStorable storable)
        {
            if (_context.IsOccupied(storable.SectionType) || storable.ItemType == ItemType.None)
            {
                return;
            }

            _itemsWaitingForStoring.Add(storable);

            if (_itemsWaitingForStoring.Count > 1)
            {
                return;
            }

            await _stateMachine.ChangeStateAsync(_storeItemState, destroyCancellationToken, true);
            await _stateMachine.ChangeStateAsync(_waitForItemState, destroyCancellationToken, true);
        }

        private async void OnSectionDataCleared(BackpackSectionType sectionType)
        {
            if (!_context.TryGetStorable(sectionType, out var storable))
            {
                return;
            }

            _itemsWaitingForCleaning.Add(storable);

            if (_itemsWaitingForCleaning.Count > 1)
            {
                return;
            }

            await _stateMachine.ChangeStateAsync(_clearSectionState, destroyCancellationToken, true);
            await _stateMachine.ChangeStateAsync(_waitForItemState, destroyCancellationToken, true);
        }
    }
}