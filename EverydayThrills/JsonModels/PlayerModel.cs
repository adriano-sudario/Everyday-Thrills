using Newtonsoft.Json;

namespace EverydayThrills.JsonModels
{
    public class PlayerModel
    {
        public string Character;
        public string AnimationName;
        public int HorizontalPosition;
        public int VerticalPosition;

        [JsonConstructor]
        public PlayerModel(string character, string animationName, int horizontalPosition, int verticalPosition)
        {
            Character = character;
            AnimationName = animationName;
            HorizontalPosition = horizontalPosition;
            VerticalPosition = verticalPosition;
        }
    }
}
