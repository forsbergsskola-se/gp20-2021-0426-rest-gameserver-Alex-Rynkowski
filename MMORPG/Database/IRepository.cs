﻿using System;
using System.Threading.Tasks;
using MMORPG.Players;

namespace MMORPG.Database{
    public interface IRepository{
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(string name);
        Task<Player> Modify(Guid id, ModifiedPlayer modifiedPlayer);
        Task<Player> Delete(Guid id);
    }
}