using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MMORPG.Controllers;

namespace MMORPG{
    public class Program{
        //my ID: Guid.Parse("0801cbfe-3867-4224-9730-b7704aec44a9");
        public static async Task Main(string[] args){
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}