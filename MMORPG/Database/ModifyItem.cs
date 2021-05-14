namespace MMORPG.Database{
    public class ModifyItem{
        public string ItemName{ get; set; }
        public ItemTypes ItemType{ get; set; }
        public int LevelRequirement{ get; set; }

        public int LevelBonus{ get; set; }
        public ItemRarity Rarity{ get; set; }
        public int SellValue{ get; set; }
    }
}