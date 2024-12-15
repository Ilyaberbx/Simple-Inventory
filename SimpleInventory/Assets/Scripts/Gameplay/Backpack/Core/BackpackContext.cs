using System;
using System.Linq;
using System.Threading.Tasks;
using Gameplay.Items;
using Gameplay.Section;
using UnityEngine;

namespace Gameplay.Backpack.Core
{
    public sealed class BackpackContext : IBackpackContext
    {
        private readonly SectionBehaviour[] _sectionBehaviours;
        public event Action<ItemType, BackpackSectionType> OnCleared;
        public event Action<ItemType, BackpackSectionType> OnStored;

        public BackpackContext(SectionBehaviour[] sectionBehaviours)
        {
            _sectionBehaviours = sectionBehaviours;
        }

        public bool TryGetStorePosition(BackpackSectionType type, out Vector3 position)
        {
            if (TryGetSection(type, out var section))
            {
                position = section.StorePosition;
                return true;
            }

            position = Vector3.zero;
            return false;
        }

        public bool TryGetClearPosition(BackpackSectionType type, out Vector3 position)
        {
            if (TryGetSection(type, out var section))
            {
                position = section.ClearPosition;
                return true;
            }

            position = Vector3.zero;
            return false;
        }

        public Task Open(BackpackSectionType type)
        {
            if (IsOpened(type))
            {
                return Task.CompletedTask;
            }

            return TryGetSection(type, out var section) ? section.Open() : Task.CompletedTask;
        }

        public Task Close(BackpackSectionType type)
        {
            if (!IsOpened(type))
            {
                return Task.CompletedTask;
            }

            return TryGetSection(type, out var section) ? section.Close() : Task.CompletedTask;
        }

        public void OpenImmediately(BackpackSectionType type)
        {
            if (IsOpened(type))
            {
                return;
            }

            if (TryGetSection(type, out var section))
            {
                section.OpenImmediately();
            }
        }

        public void CloseImmediately(BackpackSectionType type)
        {
            if (!IsOpened(type))
            {
                return;
            }

            if (TryGetSection(type, out var section))
            {
                section.CloseImmediately();
            }
        }

        public bool TryGetStorable(BackpackSectionType type, out IStorable storable)
        {
            if (!TryGetSection(type, out var section))
            {
                storable = null;
                return false;
            }

            if (section.Storable == null)
            {
                storable = null;
                return false;
            }

            storable = section.Storable;
            return true;
        }

        public void Store(BackpackSectionType type, IStorable storable)
        {
            if (!TryGetSection(type, out var section))
            {
                return;
            }

            section.Store(storable);
            OnStored?.Invoke(storable.ItemType, type);
        }

        public bool IsOpened(BackpackSectionType type)
        {
            return TryGetSection(type, out var section) && section.IsOpened;
        }

        public bool IsOccupied(BackpackSectionType type)
        {
            return TryGetSection(type, out var section) && section.IsOccupied;
        }

        public void Clear(IStorable storable, BackpackSectionType type)
        {
            if (!TryGetSection(type, out var section))
            {
                return;
            }

            section.Store(null);
            OnCleared?.Invoke(storable.ItemType, type);
        }

        private bool TryGetSection(BackpackSectionType type, out SectionBehaviour behaviour)
        {
            behaviour = _sectionBehaviours.FirstOrDefault(temp => temp.Type == type);
            return behaviour != null;
        }
    }
}