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
            CollisionOccurred += OnCollisionOccurred;
        }

        private void OnCollisionOccurred(object sender, CollisionEventArgs e)
        {
            switch (e.Player.UpdatedPosition)
            {
                case Player.UpdatedDirection.Horizontal:
                    e.Player.HorizontalPosition += e.IntersectedRectangle.Width * (e.Player.Direction.X * -1);
                    break;

                case Player.UpdatedDirection.Vertical:
                    e.Player.VerticalPosition += e.IntersectedRectangle.Height * (e.Player.Direction.Y * -1);
                    break;
            }
        }
    }
}
