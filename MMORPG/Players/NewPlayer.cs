﻿using System;
using System.Collections.Generic;
using MMORPG.Items;

namespace MMORPG.Players{
    public class NewPlayer{
        readonly string name;

        public NewPlayer(string name){
            this.name = name;
        }

        public Player SetupNewPlayer(Player player){
            player.Name = this.name;
            player.Id = Guid.NewGuid().ToString();
            player.Score = 0;
            player.Level = 0;
            player.IsDeleted = false;
            player.CreationTime = DateTime.Now;
            player.CurrentExperience = 0;
            player.ExperienceToNextLevel = 100;
            player.Inventory = new List<Item>();
            player.EquippedItems = new Dictionary<string, Item>{
                ["Sword"] = null,
                ["Offhand"] = null,
                ["Armor"] = null,
                ["Helmet"] = null
            };
            return player;
        }
    }
}