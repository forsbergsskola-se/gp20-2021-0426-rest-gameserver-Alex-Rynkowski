namespace MMORPG.Data{
    public class ModifiedItem{
        public string ItemName{ get; set; }
        public ItemTypes ItemType{ get; set; }
        public int LevelRequirement{ get; set; }

        public int LevelBonus{ get; set; }
        public ItemRarity Rarity{ get; set; }
        public int SellValue{ get; set; }
    }
}