using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayThrills.Drawables.Sceneries
{
    public static class Camera
    {
        static Vector3 position;
        static Matrix _viewMatrix;
        static int _screenWidth;
        static int _screenHeight;
        static int _bgWidth;
        static int _bgHeight;

        //public static Vector2 Position { get { return _position; } set { _position = value; } }
        public static Matrix ViewMatrix { get { return _viewMatrix; } set { _viewMatrix = value; } }
        public static int ScreenWidth { get { return _screenWidth; } set { _screenWidth = value; } }
        public static int ScreenHeight { get { return _screenHeight; } set { _screenHeight = value; } }
        public static int MapWidth { get { return _bgWidth; } set { _bgWidth = value; } }
        public static int MapHeight { get { return _bgHeight; } set { _bgHeight = value; } }

        public static void ScrollHorizontally(Vector2 playerPosition, int playerWidth, int scrollIncrement)
        {
            if (playerPosition.X + (playerWidth / 2) >= (ScreenWidth / 2) &&
                playerPosition.X + (playerWidth / 2) <= MapWidth - (ScreenWidth / 2))
                position.X += scrollIncrement * -1;
        }
        public static void ScrollVertically(Vector2 playerPosition, int playerHeight, int scrollIncrement)
        {
            if (playerPosition.Y + (playerHeight / 2) >= (ScreenHeight / 2) &&
                playerPosition.Y + (playerHeight / 2) <= MapHeight - (ScreenHeight / 2))
                position.Y += scrollIncrement * -1;
        }

        public static void SetInitialPosition(Vector2 playerPosition, int playerWidth, int playerHeight)
        {
            float positionHorizontal = (playerPosition.X - (ScreenWidth / 2) + (playerWidth / 2)) *-1;
            float minWidth = (MapWidth - (ScreenWidth / 2)) * -1;
            float maxWidth = 0;
            float positionVertical = (playerPosition.Y - (ScreenHeight / 2) + (playerHeight / 2)) * -1;
            float minHeight = (MapHeight - (ScreenHeight / 2)) * -1;
            float maxHeight = 0;
            position.X = MathHelper.Clamp(
                                          positionHorizontal,
                                          minWidth,
                                          maxWidth
                                          );
            position.Y = MathHelper.Clamp(
                                          positionVertical,
                                          minHeight,
                                          maxHeight
                                          );
        }

        public static void Update()
        {
            //if (position.X < 0)
            //    position.X = 0;
            //if (position.Y < 0)
            //    position.Y = 0;

            _viewMatrix = Matrix.CreateTranslation(position);
        }
    }
}
