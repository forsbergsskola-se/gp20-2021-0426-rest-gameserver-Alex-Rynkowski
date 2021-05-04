﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHudExplorer.API{
    public interface IRepository{
        Task<Dictionary<int, string>> GetRepositories(string user);

        Task GetRepository(string url);

        string Name{ get; set; }
    }
}