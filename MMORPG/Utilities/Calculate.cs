using MMORPG.Database;

namespace MMORPG.Utilities{
    public static class Calculate{
        public static int CalculateItemSellValue(int baseValue, int levelRequirement, ItemRarity rarity){
            return baseValue + levelRequirement * (int) rarity;
        }
    }
}