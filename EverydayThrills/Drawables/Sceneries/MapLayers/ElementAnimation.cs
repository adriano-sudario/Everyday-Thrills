using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EverydayThrills.JsonModels.AnimationModel;

namespace EverydayThrills.Drawables.Sceneries.MapLayers
{
    public class ElementAnimation
    {
        //Texture2D spriteStrip;
        //float scale;
        int elapsedTime;
        int frameDuration;
        int currentFrame;
        float? randomness;
        bool isActive;
        bool isAnimating;
        //bool isLooping;
        Rectangle sourceRectangle;
        Rectangle destinationRectangle;
        List<AnimationFrame> frames;

        public void LoadContent(List<Rectangle> sources, Vector2 position, int width, int height, float? randomness = null)
        {
            isActive = true;
            frames = new List<AnimationFrame>();
            frameDuration = 100;
            elapsedTime = 0;

            foreach (Rectangle source in sources)
            {
                AsepriteFrame aseFrame = new AsepriteFrame(source.X, source.Y, source.Width, source.Height);
                AnimationFrame frame = new AnimationFrame(aseFrame, 100);
                frames.Add(frame);
            }
            currentFrame = 0;
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            sourceRectangle = new Rectangle(frames[currentFrame].Frame.X, frames[currentFrame].Frame.Y, width, height);

            if (randomness.HasValue)
            {
                isAnimating = false;
                this.randomness = randomness.Value;
            }
            else
            {
                isAnimating = true;
            }
        }

        //private float NextFloat(Random random)
        //{
        //    double mantissa = (random.NextDouble() * 2.0) - 1.0;
        //    double exponent = Math.Pow(2.0, random.Next(-126, 128));
        //    return (float)(mantissa * exponent);
        //}

        public void Update(GameTime gameTime)
        {
            if (randomness.HasValue && !isAnimating)
            {
                Random r = new Random();
                r.Next(0, 100);
                r.NextDouble();
                //double number = r.NextDouble() * (100 - 0) + 0;
                double sortedNumber = r.NextDouble() * 100;

                if (sortedNumber <= randomness)
                    isAnimating = true;
            }

            if (isAnimating)
            {
                elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                //hasFrameChanged = false;

                if (elapsedTime > frameDuration)
                {
                    currentFrame++;

                    if (currentFrame >= frames.Count)
                    {
                        if (randomness.HasValue)
                        {
                            isAnimating = false;
                        }

                        currentFrame = 0;
                    }

                    sourceRectangle.X = frames[currentFrame].Frame.X;
                    sourceRectangle.Y = frames[currentFrame].Frame.Y;
                    sourceRectangle.Width = frames[currentFrame].Frame.Width;
                    sourceRectangle.Height = frames[currentFrame].Frame.Height;
                    //frameDuration = frames[currentFrame].Duration;
                    //frameDuration = 250;

                    elapsedTime = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Map.Atlas, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
