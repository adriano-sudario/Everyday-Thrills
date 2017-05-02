using EverydayThrills.Code;
using EverydayThrills.JsonModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EverydayThrills.JsonModels.AnimationModel;

namespace EverydayThrills.Drawables
{
    public class Animation
    {
        Texture2D spriteStrip;
        float scale;
        int elapsedTime;
        int frameDuration;
        int currentFrame;
        int[] sequence;
        bool isActive;
        bool isLooping;
        bool hasFrameChanged;
        string _animationName;
        Color color;
        Rectangle sourceRectangle;
        Vector2 position;
        AnimationFrame[] frames;
        AnimationFrameTag[] frameTags;

        public string AnimationName
        {
            get { return _animationName; }
            set
            {
                if (value != AnimationName)
                {
                    _animationName = value;
                    SetSequence();
                }
            }
        }

        public float HorizontalPosition
        {
            get { return position.X; }
            set
            {
                position.X = value;
            }
        }

        public float VerticalPosition
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        //public bool HasFrameChanged { get { return _hasFrameChanged; } set { _hasFrameChanged = value; } }

        //public AnimationFrame CollisionFrame
        //{
        //    get { return frames[collisionFrame]; }
        //}

        //public AnimationFrame CurrentFrame
        //{
        //    get { return frames[sequence[currentFrame]]; }
        //}

        //public AnimationFrame[] Frames
        //{
        //    get { return frames; }
        //}

        public Texture2D SpriteStrip
        {
            get { return spriteStrip; }
        }

        public int Width { get { return (int)(frames[sequence[currentFrame]].Frame.Width * scale); } }

        public int Height { get { return (int)(frames[sequence[currentFrame]].Frame.Height * scale); } }

        public void LoadContent(string spriteSheetName, string animationName, Vector2 position,
                                float scale = 1f, bool isLooping = true, Color? color = null)
        {
            this.scale = scale;
            this.isLooping = isLooping;
            this.color = color ?? Color.White;
            this.scale = scale;
            elapsedTime = 0;
            isActive = true;

            AnimationModel model = Loader.LoadDeserializedJsonFile<AnimationModel>(spriteSheetName);
            frames = model.Frames;
            frameTags = model.Meta.FrameTags;
            AnimationName = animationName;
            spriteStrip = Loader.LoadTexture(spriteSheetName);
            this.position = position;
            SetSequence();
            // Grab the correct frame in the image strip by multiplying the currentFrame index by the Frame width
            sourceRectangle = new Rectangle(frames[sequence[currentFrame]].Frame.X,
                                       frames[sequence[currentFrame]].Frame.Y,
                                       frames[sequence[currentFrame]].Frame.Width,
                                       frames[sequence[currentFrame]].Frame.Height);
            //int height = (int)((frames[collisionFrame].Frame.Height * scale));
            //int width = (int)((frames[collisionFrame].Frame.Width * scale));
            //collisionRectangle = new Rectangle((int)position.X,
            //                                (int)position.Y + (height / 2),
            //                                width,
            //                                height / 2);
            //destinationRectangle = new Rectangle((int)position.X,
            //                                (int)position.Y,
            //                                width,
            //                                height);
        }

        public void LoadContent(Texture2D spriteSheet, string animationName, AnimationModel model, Vector2 position,
                                float scale = 1f, bool isLooping = true, Color? color = null)
        {
            this.scale = scale;
            this.isLooping = isLooping;
            this.color = color ?? Color.White;
            this.scale = scale;
            elapsedTime = 0;
            isActive = true;

            frames = model.Frames;
            frameTags = model.Meta.FrameTags;
            AnimationName = animationName;
            spriteStrip = spriteSheet;
            this.position = position;
            SetSequence();
            // Grab the correct frame in the image strip by multiplying the currentFrame index by the Frame width
            sourceRectangle = new Rectangle(frames[sequence[currentFrame]].Frame.X,
                                       frames[sequence[currentFrame]].Frame.Y,
                                       frames[sequence[currentFrame]].Frame.Width,
                                       frames[sequence[currentFrame]].Frame.Height);
        }

        public void Update(GameTime gameTime)
        {
            if (!isActive) return;

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            hasFrameChanged = false;

            if (elapsedTime > frameDuration)
            {
                hasFrameChanged = true;
                currentFrame++;

                if (currentFrame >= sequence.Length)
                {
                    if (!isLooping)
                        currentFrame = sequence.Length - 1;
                    else
                        currentFrame = 0;
                }

                UpdateAnimationRectangles();
            }
        }

        public void SetSequence()
        {
            foreach (AnimationFrameTag frameTag in frameTags)
            {
                if (frameTag.Name == AnimationName)
                {
                    sequence = frameTag.Sequence;
                    currentFrame = 0;
                    break;
                }
            }

            UpdateAnimationRectangles();
        }

        public void UpdateAnimationRectangles()
        {
            // Grab the correct frame in the image strip by multiplying the currentFrame index by the Frame width
            sourceRectangle.X = frames[sequence[currentFrame]].Frame.X;
            sourceRectangle.Y = frames[sequence[currentFrame]].Frame.Y;
            sourceRectangle.Width = frames[sequence[currentFrame]].Frame.Width;
            sourceRectangle.Height = frames[sequence[currentFrame]].Frame.Height;
            // Update widht and height
            //destinationRect.Width = (int)(CollisionFrame.frame.w * ScaleConstant.General);
            //destinationRect.Height = (int)(CollisionFrame.frame.h * ScaleConstant.General);

            frameDuration = frames[sequence[currentFrame]].Duration;
            elapsedTime = 0;
        }

        public bool LastFrame()
        {
            if (currentFrame == sequence.Length - 1)
                return true;
            else
                return false;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteEffects effect = SpriteEffects.None)
        {
            if (!isActive)
                return;

            spriteBatch.Draw(spriteStrip, position, sourceRectangle, color, 0, 
                             Vector2.Zero, scale, effect, 0);
        }
    }
}
