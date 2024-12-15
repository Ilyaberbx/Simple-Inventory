using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Gameplay.Backpack.Core.States;
using Gameplay.Backpack.Core.States.Modules;
using Gameplay.Items;
using Gameplay.Section;
using Gameplay.Services.Items;
using Gameplay.Services.Items.Persistent;
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

        private StateMachine<BaseBackpackState> _stateMachine;
        private ItemsStorageService _storageService;
        private BackpackContext _context;
        private StoreItemState _storeState;

        private readonly List<IStorable> _itemsWaitingForCleaning = new();
        private readonly List<IStorable> _itemsWaitingForStoring = new();

        public Task SetRuntime(BackpackRuntimeData runtimeData)
        {
            return RunInitialState(runtimeData);
        }

        private async Task RunInitialState(BackpackRuntimeData runtimeData)
        {
            _stateMachine.Run();

            var waitForItemState = new WaitForItemState();
            var initializeData = new InitializeBackpackData(_sectionBehaviours, runtimeData, _context);
            var initializeState = new InitializeBackpackState(initializeData);

            await _stateMachine.ChangeStateAsync(initializeState, destroyCancellationToken);
            await _stateMachine.ChangeStateAsync(waitForItemState, destroyCancellationToken);
        }

        private void Awake()
        {
            _stateMachine = new StateMachine<BaseBackpackState>();
            _context = new BackpackContext(_sectionBehaviours);

            var loggerModule = new LoggerModule<BaseBackpackState>();
            _stateMachine.AddModule(loggerModule);

            var storeData = new StoreItemData(_storeVfxPrefab, _storeVfxPoint, _context, _itemsWaitingForStoring);
            _storeState = new StoreItemState(storeData);
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

            await _stateMachine.ChangeStateAsync(_storeState, destroyCancellationToken);
            var waitForItemState = new WaitForItemState();
            await _stateMachine.ChangeStateAsync(waitForItemState, destroyCancellationToken);
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

            var clearSectionState = new ClearSectionState(_itemsWaitingForCleaning, _context);
            await _stateMachine.ChangeStateAsync(clearSectionState, destroyCancellationToken);
            var waitForItemState = new WaitForItemState();
            await _stateMachine.ChangeStateAsync(waitForItemState, destroyCancellationToken);
        }
    }
}