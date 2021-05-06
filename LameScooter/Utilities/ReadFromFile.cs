using System.IO;
using System.Threading.Tasks;

namespace LameScooter.Utilities{
    public static class ReadFromFile{
        public static async Task<string> ReadFile(string path){
            return await File.ReadAllTextAsync(path);
        }
    }
}