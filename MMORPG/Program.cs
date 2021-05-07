using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MMORPG.Controllers;

namespace MMORPG{
    public class Program{
        public static void Main(string[] args){
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }

    public class Player{
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public int Score{ get; set; }
        public int Level{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreationTime{ get; set; }
        public IItem Item{ get; set; }

        public IItem GetItems(Guid playerId){
            return this.Item;
        }
    }

    public class NewPlayer{
        public string Name{ get; set; }
    }

    public class ModifiedPlayer{
        public int Score{ get; set; }
    }

    public interface IRepository{
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);
    }

    public class FileRepository : IRepository{
        public Task<Player> Get(Guid id){
            throw new NotImplementedException();
        }

        public Task<Player[]> GetAll(){
            throw new NotImplementedException();
        }

        public Task<Player> Create(Player player){
            throw new NotImplementedException();
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id){
            throw new NotImplementedException();
        }
    }

    public class PlayerController{
        IRepository repository;

        public PlayerController(IRepository repository){
            this.repository = repository;
        }
    }

    public interface IItem{
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public int Level{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreationTime{ get; set; }
    }

    public class Item : IItem{
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public int Level{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreationTime{ get; set; }
    }

    public class MongoDbRepository : IRepository{
        public Task<Player> Get(Guid id){
            throw new NotImplementedException();
        }

        public Task<Player[]> GetAll(){
            throw new NotImplementedException();
        }

        public Task<Player> Create(Player player){
            throw new NotImplementedException();
        }

        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id){
            throw new NotImplementedException();
        }
    }

    public class NotFoundException : Exception{
        public NotFoundException(){
        }

        public NotFoundException(string message) : base(){
        }
    }
}