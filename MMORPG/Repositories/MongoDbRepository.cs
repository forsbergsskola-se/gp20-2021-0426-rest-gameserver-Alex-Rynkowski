namespace MMORPG.Repositories{
    public class MongoDbRepository : IRepository{
        public IPlayerRepository PlayerRepository => new MongoDbPlayerRepository();
        public IEquipRepository EquipRepository => new MongoDbEquipRepository();
        public IItemRepository ItemRepository => new MongoDbItemRepository();
        public IQuestRepository QuestRepository => new MongoDbQuestRepository();
        public IStatistics Statistics => new MongoDbStatistics();
        public ILeaderboard Leaderboard => new MongoDbLeaderboard();
        public IDrop DropCollection => new MongoDbDropCollection();
    }
}