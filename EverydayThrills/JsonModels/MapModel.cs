using EverydayThrills.Code;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayThrills.JsonModels
{
    public class MapModel
    {
        public string Name;
        public int Width;
        public int Height;
        public Layer[] Layers;

        public enum LayerType { Parallax, Background, SceneryElements, PathBlock, Transition }

        //public enum LayerSubType { Parallax, Background, SceneryElements, PathBlock, Transition }

        [JsonConstructor]
        public MapModel(string name, Layer[] layers, int tilewidth, int width, int height)
        {
            Name = name;
            Width = (int)((width * tilewidth) * GlobalConstants.Scale);
            Height = (int)((height * tilewidth) * GlobalConstants.Scale);
            Layers = layers;
        }

        public class Layer
        {
            public string Name;
            public LayerObject[] Objects;
            public LayerType LayerType;

            [JsonConstructor]
            public Layer(string name, LayerObject[] objects)
            {
                try
                {
                    Name = name;
                    Objects = objects;
                    SetType(name);
                }
                catch (Exception ex)
                {
                    string pqp = ex.Message;
                }
            }

            public void SetType(string name)
            {
                switch (name)
                {
                    case "parallax":
                        LayerType = LayerType.Parallax;
                        break;

                    case "background":
                        LayerType = LayerType.Background;
                        break;

                    case "sceneryElements":
                        LayerType = LayerType.SceneryElements;
                        break;

                    case "pathBlock":
                        LayerType = LayerType.PathBlock;
                        break;

                    case "transition":
                        LayerType = LayerType.Transition;
                        break;
                }
            }
        }

        public class LayerObject
        {
            public int Id;
            public int Width;
            public int Height;
            public int X;
            public int Y;
            public string Type;
            public Rectangle DestinationRectangle;

            public string Direction;
            public float? MoveSpeed;
            public Rectangle? Source;
            public Rectangle? Collision;
            public int? ToId;
            public string ToMap;

            public float? AnimationRandomness;
            public List<Rectangle> AnimationSequence;

            [JsonConstructor]
            public LayerObject(int id, int width, int height, int x, int y, string type, 
                               LayerCustomProperties properties, LayerAnimationInfo animation)
            {
                bool isScaled = true;
                Id = id;
                Width = width;
                Height = height;
                X = x;
                Y = y;
                if (type != "collision")
                    Y -= height;
                SetDestinationRectangle(isScaled);
                Type = type;

                if (properties != null)
                {
                    Direction = properties.Direction;
                    MoveSpeed = properties.MoveSpeed;
                    if (properties.Source.HasValue)
                        Source = new Rectangle((int)properties.Source.Value.X, (int)properties.Source.Value.Y,
                                               Width, Height);
                    if (properties.Collision.HasValue)
                    {
                        Collision = new Rectangle(
                                                  (int)(properties.Collision.Value.X * GlobalConstants.Scale) + 
                                                  DestinationRectangle.X, 
                                                  (int)(properties.Collision.Value.Y * GlobalConstants.Scale) + 
                                                  DestinationRectangle.Y,
                                                  (int)(properties.Collision.Value.Width * GlobalConstants.Scale),
                                                  (int)(properties.Collision.Value.Height * GlobalConstants.Scale)
                                                  );
                    }
                    ToId = properties.ToId;
                    ToMap = properties.ToMap;
                }

                if (animation != null)
                {
                    AnimationRandomness = animation.Randomness;
                    LayerAnimationSource[] animationSequenceSource = animation.Sequence;
                    AnimationSequence = new List<Rectangle>();

                    foreach (LayerAnimationSource s in animationSequenceSource)
                    {
                        AnimationSequence.Add(new Rectangle((int)s.Source.X, (int)s.Source.Y, Width, Height));
                    }
                }
            }

            public void SetDestinationRectangle(bool isScaled)
            {
                int destinationWidth = Width;
                int destinationHeight = Height;
                if (isScaled)
                {
                    destinationWidth = (int)(Width * GlobalConstants.Scale);
                    destinationHeight = (int)(Height * GlobalConstants.Scale);
                }
                DestinationRectangle = new Rectangle(
                                                     (int)(X * GlobalConstants.Scale),
                                                     (int)(Y * GlobalConstants.Scale),
                                                     destinationWidth,
                                                     destinationHeight
                                                    );
            }
        }

        public class LayerCustomProperties
        {
            public string Direction;
            public float? MoveSpeed;
            public Vector2? Source;
            public Rectangle? Collision;
            public int? ToId;
            public string ToMap;

            [JsonConstructor]
            public LayerCustomProperties(string direction, float moveSpeed, string source, string collision,
                                         int toId, string toMap)
            {
                Direction = direction;
                MoveSpeed = moveSpeed;
                Source = StringToVector2(source);
                Collision = StringToRectangle(collision);
                ToId = toId;
                ToMap = toMap;
            }

            private Rectangle? StringToRectangle(string rectangle)
            {
                if (rectangle == "" || rectangle == null)
                    return null;
                string[] split = rectangle.Split('-');
                return new Rectangle(int.Parse(split[0]), int.Parse(split[1]),
                                     int.Parse(split[2]), int.Parse(split[3]));
            }

            private Vector2? StringToVector2(string vector)
            {
                if (vector == "" || vector == null)
                    return null;
                string[] split = vector.Split('-');
                return new Vector2(int.Parse(split[0]), int.Parse(split[1]));
            }
        }
        public class LayerAnimationInfo
        {
            public float Randomness;
            public LayerAnimationSource[] Sequence;

            [JsonConstructor]
            public LayerAnimationInfo(float randomness, LayerAnimationSource[] sources)
            {
                Randomness = randomness;
                Sequence = sources;
            }
        }

        public class LayerAnimationSource
        {
            public Vector2 Source;

            [JsonConstructor]
            public LayerAnimationSource(int x, int y)
            {
                Source = new Vector2(x, y);
            }
        }
    }
}
