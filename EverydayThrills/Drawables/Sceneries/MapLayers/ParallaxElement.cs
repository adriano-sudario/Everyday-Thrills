using EverydayThrills.Drawables.Sceneries.MapCollisions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayThrills.Drawables.Sceneries.MapLayers
{
    public class ParallaxElement : MapElement
    {
        private float moveSpeed;
        private int width;
        private Vector2 position;
        private Vector2 positionBackup;
        private HorizontalDirection horizontalDirection;
        //private VerticalDirection verticalDirection;

        public enum HorizontalDirection { Left, Right };
        //public enum VerticalDirection { None, Up, Down };

        public ParallaxElement(float moveSpeed, HorizontalDirection horizontalDirection,
            Rectangle source, Rectangle destination) : 
            base(source, destination)
        {
            SetAttributes(moveSpeed, horizontalDirection, destination);
        }

        public ParallaxElement(float moveSpeed, HorizontalDirection horizontalDirection,
            Rectangle source, Rectangle destination, ElementAnimation animation) :
            base(source, destination, animation)
        {
            SetAttributes(moveSpeed, horizontalDirection, destination);
        }

        public void SetAttributes(float moveSpeed, HorizontalDirection horizontalDirection, Rectangle destination)
        {
            position = new Vector2(destination.X, destination.Y);
            positionBackup = position;
            width = destination.Width;
            this.moveSpeed = moveSpeed;
            this.horizontalDirection = horizontalDirection;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Move();
        }

        private void Move()
        {
            switch (horizontalDirection)
            {
                case HorizontalDirection.Left:
                    position.X += moveSpeed;
                    break;

                case HorizontalDirection.Right:
                    position.X -= moveSpeed;
                    break;
            }

            if (position.X > positionBackup.X + width || position.X < positionBackup.X - width)
                position.X = positionBackup.X;

            Destination.X = (int)position.X;
        }
    }
}
