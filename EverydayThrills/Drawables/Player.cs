using EverydayThrills.Code;
using EverydayThrills.Drawables.Sceneries;
using EverydayThrills.Inputs.Interface;
using EverydayThrills.JsonModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EverydayThrills.Drawables
{
    public class Player
    {
        Vector2 position;
        Animation animation { get; set; }
        IInput input;
        float scale;
        int hp;

        public float Movement { get; set; }

        public Map Map { get; set; }

        public Vector2 Direction;
        public Vector2 PreviewDirection;

        public UpdatedDirection UpdatedPosition;

        public enum UpdatedDirection { None, Horizontal, Vertical };

        public string AnimationName
        {
            get { return animation.AnimationName; }
            set
            {
                animation.AnimationName = value;
            }
        }

        public string DirectionName
        {
            get { return AnimationName.Split('_')[1]; }
        }

        public int Width
        {
            get { return (animation.Width); }
        }

        public int Height
        {
            get { return (animation.Height); }
        }

        public float HorizontalPosition
        {
            get { return position.X; }
            set
            {
                int increment = (int)(value - position.X);
                CollisionRectangle.X += increment;
                position.X = value;
                animation.HorizontalPosition = value;
                Camera.ScrollHorizontally(position, Width, increment);

                if (Direction.Y == 0 || AnimationName.Split('_')[0] == "idle" || 
                    (PreviewDirection.X != Direction.X && Direction.X != 0))
                {
                    if (Direction.X > 0)
                        AnimationName = "walk_right";
                    else if (Direction.X < 0)
                        AnimationName = "walk_left";
                }
            }
        }

        public float VerticalPosition
        {
            get { return position.Y; }
            set
            {
                int increment = (int)(value - position.Y);
                CollisionRectangle.Y += increment;
                position.Y = value;
                animation.VerticalPosition = value;
                Camera.ScrollVertically(position, Height, increment);

                if (Direction.X == 0 || AnimationName.Split('_')[0] == "idle" ||
                    (PreviewDirection.Y != Direction.Y && Direction.Y != 0))
                {
                    if (Direction.Y > 0)
                        AnimationName = "walk_down";
                    else if (Direction.Y < 0)
                        AnimationName = "walk_up";
                }
            }
        }

        public Rectangle CollisionRectangle;

        public Animation Animation
        {
            get { return animation; }
        }

        public void LoadContent(PlayerModel data, IInput input,
                                float scale = 1f)
        {
            this.input = input;
            this.scale = scale * GlobalConstants.Scale;
            animation = new Animation();
            position = new Vector2(
                                   (int)(data.HorizontalPosition * GlobalConstants.Scale),
                                   (int)(data.VerticalPosition * GlobalConstants.Scale)
                                   );
            animation.LoadContent(data.Character, data.AnimationName, position, this.scale);
            AnimationName = data.AnimationName;
            Movement = 3f;
            CollisionRectangle = new Rectangle((int)(HorizontalPosition + (data.CollisionRectangle.X * this.scale)),
                                               (int)(VerticalPosition + (data.CollisionRectangle.Y * this.scale)),
                                               (int)(data.CollisionRectangle.Width * this.scale),
                                               (int)(data.CollisionRectangle.Height * this.scale));
        }

        public void Update(GameTime gameTime)
        {
            CheckInputs();

            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }

        public void CheckInputs()
        {
            input.GetInputs();

            PreviewDirection = Direction;
            Direction = new Vector2(input.MoveX(), input.MoveY());

            if (Direction.X != 0 || Direction.Y != 0)
                Walk();
            else
                AnimationName = "idle_" + DirectionName;
        }

        public void Walk()
        {
            if (Direction.X != 0)
            {
                HorizontalPosition += (Movement * Direction.X);
                CollisionCheck(UpdatedDirection.Horizontal);
            }

            if (Direction.Y != 0)
            {
                VerticalPosition += (Movement * Direction.Y);
                CollisionCheck(UpdatedDirection.Vertical);
            }
        }

        public void CollisionCheck(UpdatedDirection updatedPosition)
        {
            UpdatedPosition = updatedPosition;
            Map.CollisionCheck();
            UpdatedPosition = UpdatedDirection.None;
        }
    }
}
