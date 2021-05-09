using System;

namespace MMORPG.Items{
    public interface IItem{
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public int LevelRequirement{ get; set; }
        public int SellValue{ get; set; }
        public int LevelBonus{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreationTime{ get; set; }
        public string Rarity{ get; set; }
        public string Category{ get; }
        public string Type{ get; set; }
    }

    public enum ItemRarity{
        Common,
        Uncommon,
        Rare,
        Epic
    }
}