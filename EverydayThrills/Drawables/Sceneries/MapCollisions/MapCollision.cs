﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayThrills.Drawables.Sceneries.MapCollisions
{
    public class MapCollision
    {
        public class CollisionEventArgs : EventArgs
        {
            public Player Player { get; set; }
            public Map Map { get; set; }
            public Rectangle RectangleCollided { get; set; }
            public Rectangle IntersectedRectangle { get; set; }
        }

        public Rectangle Rectangle;
        public virtual event EventHandler<CollisionEventArgs> CollisionOccurred;

        public MapCollision(Rectangle collisionRectangle)
        {
            Rectangle = collisionRectangle;
        }

        public void Check(Player player, Map map)
        {
            Rectangle intersectedRectangle = Rectangle.Intersect(player.CollisionRectangle, Rectangle);

            if (!intersectedRectangle.IsEmpty)
            {
                CollisionEventArgs args = new CollisionEventArgs();
                args.Player = player;
                args.Map = map;
                args.RectangleCollided = Rectangle;
                args.IntersectedRectangle = intersectedRectangle;
                OnCollisionOccurred(args);
            }
        }

        public void Check(Player player, List<MapCollision> collisions)
        {
            Rectangle playerCollisionRectangle = player.CollisionRectangle;

            foreach (MapCollision collision in collisions)
            {
                if (collision.Rectangle.Intersects(playerCollisionRectangle))
                {
                    CollisionEventArgs args = new CollisionEventArgs();
                    args.Player = player;
                    args.RectangleCollided = Rectangle;
                    OnCollisionOccurred(args);
                }
            }
        }

        protected virtual void OnCollisionOccurred(CollisionEventArgs args)
        {
            CollisionOccurred?.Invoke(this, args);
        }
    }
}
