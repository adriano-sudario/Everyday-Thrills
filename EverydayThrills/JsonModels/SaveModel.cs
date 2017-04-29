using Newtonsoft.Json;

namespace EverydayThrills.JsonModels
{
    class SaveModel
    {
        public PlayerModel Player;
        public string Location;

        [JsonConstructor]
        public SaveModel(PlayerModel player, string location)
        {
            Player = player;
            Location = location;
        }
    }
}
