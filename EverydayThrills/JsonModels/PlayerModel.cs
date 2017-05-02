using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace EverydayThrills.JsonModels
{
    public class PlayerModel
    {
        public string Character;
        public string AnimationName;
        public int HorizontalPosition;
        public int VerticalPosition;
        public Rectangle CollisionRectangle;

        [JsonConstructor]
        public PlayerModel(string character, string animationName, int horizontalPosition, int verticalPosition,
                           string collision)
        {
            Character = character;
            AnimationName = animationName;
            HorizontalPosition = horizontalPosition;
            VerticalPosition = verticalPosition;
            CollisionRectangle = StringToRectangle(collision);
        }

        private Rectangle StringToRectangle(string rectangle)
        {
            //if (rectangle == "" || rectangle == null)
            //    return null;
            string[] split = rectangle.Split('-');
            return new Rectangle(int.Parse(split[0]), int.Parse(split[1]),
                                 int.Parse(split[2]), int.Parse(split[3]));
        }
    }
}
