using MMORPG.Database;

namespace MMORPG.Utilities{
    public static class Calculate{
        public static int CalculateItemSellValue(int baseValue, int levelRequirement, ItemRarity rarity){
            return baseValue + levelRequirement * (int) rarity;
        }

        public static bool CanAffordLevel(int level, int gold){
            return (level + 1) * 100 < gold;
        }
    }
}