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
            Width = width * tilewidth;
            Height = height * tilewidth;
            Layers = layers;
        }

        public class Layer
        {
            public string Name;
            public LayerObject[] Objects;
            public LayerType Type;

            [JsonConstructor]
            public Layer(string name, LayerObject[] objects)
            {
                Name = name;
                Objects = objects;
                SetType(name);
            }

            public void SetType(string name)
            {
                switch (name)
                {
                    case "parallax":
                        Type = LayerType.Parallax;
                        break;

                    case "background":
                        Type = LayerType.Background;
                        break;

                    case "sceneryElements":
                        Type = LayerType.SceneryElements;
                        break;

                    case "pathBlock":
                        Type = LayerType.PathBlock;
                        break;

                    case "transition":
                        Type = LayerType.Transition;
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
                               LayerCustomProperties properties)
            {
                Id = id;
                Width = width;
                Height = height;
                X = x;
                Y = y;
                Type = type;

                Direction = properties.Direction;
                MoveSpeed = properties.MoveSpeed;
                if (properties.Source != null)
                    Source = new Rectangle((int)properties.Source.Value.X, (int)properties.Source.Value.X,
                                           Width, Height);
                if (properties.Collision != null)
                    Collision = properties.Collision.Value;
                ToId = properties.ToId;
                ToMap = properties.ToMap;

                AnimationRandomness = properties.AnimationRandomness;
                foreach (LayerAnimationSource s in properties.AnimationSequenceSource)
                {
                    AnimationSequence.Add(new Rectangle((int)s.Source.X, (int)s.Source.Y, Width, Height));
                }
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

            public float? AnimationRandomness;
            public LayerAnimationSource[] AnimationSequenceSource;

            [JsonConstructor]
            public LayerCustomProperties(string direction, float moveSpeed, string source, string collision,
                                         int toId, string toMap, LayerAnimationInfo animation)
            {
                Direction = direction;
                MoveSpeed = moveSpeed;
                Source = StringToVector2(source);
                Collision = StringToRectangle(collision);
                ToId = toId;
                ToMap = toMap;
                if (animation == null)
                    return;
                AnimationRandomness = animation.Randomness;
                AnimationSequenceSource = animation.Sequence;
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
