using System;
using System.Collections.Generic;
using MMORPG.BLL;

namespace MMORPG.Data{
    public class NewPlayer{
        public string Name{ get; set; }

        public NewPlayer(string name){
            this.Name = name;
        }

        public Player SetupNewPlayer(Player player){
            player.Name = this.Name;
            player.Id = Guid.NewGuid();
            player.Score = 0;
            player.Level = 0;
            player.IsDeleted = false;
            player.CreationTime = DateTime.Now;
            player.CurrentExperience = 0;
            player.ExperienceToNextLevel = 100;
            player.Inventory = new List<Item>();
            player.EquippedItems = new Dictionary<string, Item>{
                [ItemTypes.Sword.ToString()] = null,
                [ItemTypes.Shield.ToString()] = null,
                [ItemTypes.Armor.ToString()] = null,
                [ItemTypes.Helmet.ToString()] = null
            };
            player.Quests = new Quest[5];
            return player;
        }
    }
}