using Gameplay.Items;
using UnityEngine;

namespace Gameplay.Services.Items.Configuration
{
    [CreateAssetMenu(menuName = "Create ItemsConfigurationServiceSettings",
        fileName = "ItemsConfigurationServiceSettings", order = 0)]
    public class ItemsConfigurationServiceSettings : ScriptableObject
    {
        [SerializeField] private ItemConfiguration[] _configurations;

        public ItemConfiguration[] Configurations => _configurations;
    }
}