namespace MMORPG.Repositories{
    public interface IRepository{
        IPlayerRepository PlayerRepository{ get; }
        IEquipRepository EquipRepository{ get; }
        IItemRepository ItemRepository{ get; }
        IQuestRepository QuestRepository{ get; }
        
        IStatistics Statistics{ get; }
    }
}