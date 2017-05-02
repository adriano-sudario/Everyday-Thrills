using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EverydayThrills.Drawables;
using EverydayThrills.Code;
using EverydayThrills.Screens;
using EverydayThrills.Drawables.Sceneries;
using EverydayThrills.Inputs.Interface;
using EverydayThrills.Inputs;

namespace EverydayThrills
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

        private static int _screenWidth;
        private static int _screenHeight;

        public static int ScreenWidth { get { return _screenWidth; } }

        public static int ScreenHeight { get { return _screenHeight; } }

        //Player player;
        Exploration explorationScreen;

        public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
            //800 px - 50 tiles (16px tiles)
            _screenWidth = GraphicsDevice.Viewport.Width;
            //480 px - 30 tiles (16px tiles)
            _screenHeight = GraphicsDevice.Viewport.Height;

            //Player player = new Player();

            Loader.Initialize(Content);

            base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

            Player player = new Player();
            Map map = new Map();
            IInput input = new KeyboardInput();
            player.LoadContent(Loader.SaveData.Player, input);
            map.LoadContent(player, Loader.SaveData.Location);
            player.Map = map;

            explorationScreen = new Exploration(player, map);
        }

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

            explorationScreen.Update(gameTime);

            base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            explorationScreen.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
		}
	}
}
