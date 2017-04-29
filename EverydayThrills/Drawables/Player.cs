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

        public float movement { get; set; }

        public string AnimationName
        {
            get { return animation.AnimationName; }
            set
            {
                animation.AnimationName = value;
            }
        }

        public string Direction
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
                position.X = value;
                //animation.
            }
        }

        public float VerticalPosition
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Rectangle CollisionRectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, Width, Height); }
        }

        public Animation Animation
        {
            get { return animation; }
        }

        public void LoadContent(PlayerModel data, IInput input,
                                float scale = 1f)
        {
            this.input = input;
            this.scale = scale;
            animation = new Animation();
            position = new Vector2(data.HorizontalPosition, data.VerticalPosition);
            animation.LoadContent(data.Character, data.AnimationName, position);
            AnimationName = data.AnimationName;
            movement = 1f;
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

            Vector2 walkValue = new Vector2(input.MoveX(), input.MoveY());

            if (walkValue.X != 0 || walkValue.Y != 0)
            {
                Walk(walkValue);
            }
            else
            {
                AnimationName = "idle_" + Direction;
            }
        }

        public void Walk(Vector2 multiplier)
        {
            string direction = "";

            if (multiplier.X != 0)
            {
                position.X += (movement * multiplier.X);
                if (multiplier.X > 0)
                    direction = "right";
                else if (multiplier.X < 0)
                    direction = "left";
                //Map.UpdatePlayerAfterMovement(this, direction);
            }

            if (multiplier.Y != 0)
            {
                position.Y += (movement * multiplier.Y);
                if (multiplier.Y > 0)
                    direction = "down";
                else if (multiplier.Y < 0)
                    direction = "up";
                //Map.UpdatePlayerAfterMovement(this, direction);
            }

            if (multiplier.X > 0)
                AnimationName = "walk_right";
            else if (multiplier.X < 0)
                AnimationName = "walk_left";
            else if (multiplier.Y > 0)
                AnimationName = "walk_down";
            else if (multiplier.Y < 0)
                AnimationName = "walk_up";
        }
    }
}
