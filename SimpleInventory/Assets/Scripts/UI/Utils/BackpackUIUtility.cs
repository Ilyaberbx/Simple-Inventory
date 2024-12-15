using System.Collections.Generic;
using Better.Locators.Runtime;
using Gameplay.Items;
using Gameplay.Services.Items;
using Services;
using UI.BackpackInfo;
using UI.Sections;

namespace UI.Utils
{
    public static class BackpackUIUtility
    {
        private static readonly ServiceProperty<UserService> UserServiceProperty = new();
        private static readonly ServiceProperty<ItemsConfigurationService> ItemsConfigurationServiceProperty = new();
        private static readonly ServiceProperty<ItemsSpriteService> SpritesServiceProperty = new();

        public static BackpackInfoModel CreateInfoModel()
        {
            var spritesService = SpritesServiceProperty.CachedService;
            var configurationService = ItemsConfigurationServiceProperty.CachedService;
            var userService = UserServiceProperty.CachedService;

            var backpackProperty = userService.BackpackDataProperty;
            var backpackRuntime = backpackProperty.Value;
            var sectionModels = new List<SectionInfoModel>();

            foreach (var sectionRuntime in backpackRuntime.Sections)
            {
                var itemType = sectionRuntime.Item;
                var sectionType = sectionRuntime.SectionType;

                if (itemType == ItemType.None)
                {
                    var emptySectionModel = new SectionInfoModel(null, itemType, sectionType, string.Empty, 0);
                    sectionModels.Add(emptySectionModel);
                    continue;
                }

                var configuration = configurationService.GetConfiguration(itemType);
                var itemSprite = spritesService.GetSprite(itemType);
                var name = configuration.Name;
                var weight = configuration.Weight;

                var sectionModel = new SectionInfoModel(itemSprite, itemType, sectionType, name, weight);
                sectionModels.Add(sectionModel);
            }

            return new BackpackInfoModel(sectionModels.ToArray());
        }
    }
}