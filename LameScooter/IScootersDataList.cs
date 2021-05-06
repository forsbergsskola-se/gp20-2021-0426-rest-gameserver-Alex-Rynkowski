namespace LameScooter{
    public interface IScootersDataList{
        string Id{ get; set; }
        string Name{ get; set; }
        float X{ get; set; }
        float Y{ get; set; }
        int BikesAvailable{ get; set; }
        int SpacesAvailable{ get; set; }
        int Capacity{ get; set; }
        bool AllowDropoff{ get; set; }
        bool AllowOverloading{ get; set; }
        bool IsFloatingBike{ get; set; }
        bool IsCarStation{ get; set; }
        string State{ get; set; }
    }
}