namespace Client.Model{
    public class Item{
        public string ItemName{ get; set; }
        public ItemTypes ItemType{ get; set; }
        public int LevelRequirement{ get; set; }
        public int LevelBonus{ get; set; }
        public ItemRarity Rarity{ get; set; }
        public int SellValue{ get; set; }
    }

    public enum ItemRarity{
        Common,
        Uncommon,
        Rare,
        Epic
    }

    public enum ItemTypes{
        Sword,
        Shield,
        Armor,
        Helmet,
    }
}