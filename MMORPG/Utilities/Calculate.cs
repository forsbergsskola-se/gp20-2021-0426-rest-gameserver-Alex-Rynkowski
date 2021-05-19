using MMORPG.Data;

namespace MMORPG.Utilities{
    public static class Calculate{
        public static bool CanAffordLevel(int level, int gold){
            return (level + 1) * 100 < gold;
        }
    }
}