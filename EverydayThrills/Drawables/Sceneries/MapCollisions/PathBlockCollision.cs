using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayThrills.Drawables.Sceneries.MapCollisions
{
    public class PathBlockCollision : MapCollision
    {
        public PathBlockCollision(Rectangle collisionRectangle) : base (collisionRectangle)
        {
            CollisionOccurred += PathBlockCollision_CollisionOccurred;
        }

        private void PathBlockCollision_CollisionOccurred(object sender, CollisionEventArgs e)
        {
            switch (e.Player.Direction)
            {
                case "right":
                    e.Player.HorizontalPosition = e.RectangleCollided.Left - e.Player.Width;
                    break;

                case "left":
                    e.Player.HorizontalPosition = e.RectangleCollided.Right;
                    break;

                case "down":
                    e.Player.VerticalPosition = e.RectangleCollided.Top - e.Player.Height;
                    break;

                case "up":
                    e.Player.VerticalPosition = e.RectangleCollided.Bottom;
                    break;
            }
        }
    }
}
