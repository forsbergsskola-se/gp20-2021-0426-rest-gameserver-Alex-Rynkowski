using System;

namespace MMORPG.Items{
    public interface IItem{
        public string ItemName{ get; set; }
        public string ItemType{ get; set; }

        public int LevelRequirement{ get; set; }
        public int LevelBonus{ get; set; }
        public string Rarity{ get; set; }

        public bool IsDeleted{ get; set; }
        public int SellValue{ get; }
    }
}