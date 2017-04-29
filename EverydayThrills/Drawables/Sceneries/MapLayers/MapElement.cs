using EverydayThrills.Drawables.Sceneries.MapCollisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayThrills.Drawables.Sceneries.MapLayers
{
    public class MapElement
    {
        public Rectangle Source;
        public Rectangle Destination;
        public ElementAnimation Animation;
        public MapCollision Collision;

        public MapElement(Rectangle source, Rectangle destination)
        {
            SetSourceAndDestination(source, destination);
        }

        public MapElement(Rectangle source, Rectangle destination, ElementAnimation animation)
        {
            SetSourceAndDestination(source, destination);
            Animation = animation;
        }

        public MapElement(Rectangle source, Rectangle destination, MapCollision collision)
        {
            SetSourceAndDestination(source, destination);
            Collision = collision;
        }

        public MapElement(Rectangle source, Rectangle destination, ElementAnimation animation, MapCollision collision)
        {
            SetSourceAndDestination(source, destination);
            Animation = animation;
            Collision = collision;
        }

        public void SetSourceAndDestination(Rectangle source, Rectangle destination)
        {
            Source = source;
            Destination = destination;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Animation != null)
                Animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Animation != null)
                Animation.Draw(spriteBatch);
            else
                spriteBatch.Draw(Map.Atlas, Destination, Source, Color.White);
        }
    }
}
