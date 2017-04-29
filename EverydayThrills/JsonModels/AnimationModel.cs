using Newtonsoft.Json;

namespace EverydayThrills.JsonModels
{
    public class AnimationModel
    {
        public AnimationFrame[] Frames;
        public AnimationMeta Meta;

        [JsonConstructor]
        public AnimationModel(AnimationFrame[] frames, AnimationMeta meta)
        {
            Frames = frames;
            Meta = meta;
        }

        public class AnimationFrame
        {
            public AsepriteFrame Frame;
            public int Duration;

            [JsonConstructor]
            public AnimationFrame(AsepriteFrame frame, int duration)
            {
                Frame = frame;
                Duration = duration;
            }
        }

        public class AsepriteFrame
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;

            [JsonConstructor]
            public AsepriteFrame(int x, int y, int w, int h)
            {
                X = x;
                Y = y;
                Width = w;
                Height = h;
            }
        }

        public class AnimationMeta
        {
            public AnimationFrameTag[] FrameTags;

            [JsonConstructor]
            public AnimationMeta(AnimationFrameTag[] frameTags)
            {
                FrameTags = frameTags;
            }
        }

        public class AnimationFrameTag
        {
            public string Name;
            public int[] Sequence;

            [JsonConstructor]
            public AnimationFrameTag(string name, int[] sequence)
            {
                Name = name;
                Sequence = sequence;
            }
        }
    }
}
