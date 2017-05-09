using EverydayThrills.Drawables;
using EverydayThrills.Drawables.Sceneries;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EverydayThrills.Screens
{
    public class Exploration
    {
        Player player;
        Map map;

        public Exploration(Player player, Map map)
        {
            this.player = player;
            this.map = map;
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            map.Update(gameTime);
            Camera.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //player.Draw(spriteBatch);
            map.Draw(spriteBatch);
        }
    }
}
