using EverydayThrills.Code;
using EverydayThrills.Drawables.Sceneries.MapCollisions;
using EverydayThrills.Drawables.Sceneries.MapLayers;
using EverydayThrills.JsonModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EverydayThrills.JsonModels.MapModel;

namespace EverydayThrills.Drawables.Sceneries
{
    public class Map
    {
        static Texture2D atlas;
        Player player;
        List<MapCollision> collisions;
        List<MapElement> backgroundElements;
        List<MapElement> middlegroundElements;
        List<MapElement> frontgroundElements;
        private int width;
        private int height;
        private string name;

        public int Width { get { return width; } }
        public int Height { get { return width; } }
        public string Name { get { return name; } }
        public static Texture2D Atlas { get { return atlas; } }

        public void LoadContent(Player player, string name)
        {
            this.player = player;
            MapModel model = Loader.LoadDeserializedJsonFile<MapModel>(name);
            SetElements(model);
            width = model.Width;
            height = model.Height;
            Camera.MapWidth = width;
            Camera.MapHeight = height;
            Camera.SetInitialPosition(new Vector2(player.HorizontalPosition, player.VerticalPosition),
                                      player.Width, player.Height);
            name = model.Name;
            atlas = Loader.LoadTexture("mapAtlas");
            ////List<int[]> collisionArray = (List<int[]>)Utilities.LoadDeserializedJsonFile<object>(model.dataArrayFileName);
            //collisionRectangles = GetRectanglesByArray(model.dataArray, 1);
        }

        public void SetElements(MapModel model)
        {
            backgroundElements = new List<MapElement>();
            middlegroundElements = new List<MapElement>();
            frontgroundElements = new List<MapElement>();
            collisions = new List<MapCollision>();

            foreach (Layer layer in model.Layers)
            {
                AddElement(layer.Objects, layer.LayerType);
            }
        }

        public void AddElement(LayerObject[] layerObjects, LayerType type)
        {
            foreach (LayerObject layerObject in layerObjects)
            {
                MapCollision collision = null;
                MapElement mapElement = null;

                switch (type)
                {
                    case LayerType.Background:
                        mapElement = new MapElement(layerObject.Source.Value, layerObject.DestinationRectangle);
                        backgroundElements.Add(mapElement);
                        break;

                    case LayerType.Parallax:
                        ParallaxElement.HorizontalDirection parallaxDirection = ParallaxElement.HorizontalDirection.Left;
                        if (layerObject.Direction == "right")
                            parallaxDirection = ParallaxElement.HorizontalDirection.Right;
                        mapElement = new ParallaxElement(layerObject.MoveSpeed.Value,
                                                         parallaxDirection,
                                                         layerObject.Source.Value, layerObject.DestinationRectangle);
                        backgroundElements.Add(mapElement);
                        break;

                    case LayerType.PathBlock:
                        collision = new PathBlockCollision(layerObject.DestinationRectangle);
                        collisions.Add(collision);
                        break;

                    case LayerType.SceneryElements:
                        if (layerObject.Collision != null)
                        {
                            collision = new PathBlockCollision(layerObject.Collision.Value);
                            collisions.Add(collision);
                            if (layerObject.AnimationSequence != null)
                            {
                                ElementAnimation animation = GetAnimation(layerObject);
                                mapElement = new MapElement(
                                                            layerObject.Source.Value, 
                                                            layerObject.DestinationRectangle, 
                                                            animation, collision
                                                            );
                            }
                            else
                            {
                                mapElement = new MapElement(layerObject.Source.Value, 
                                                            layerObject.DestinationRectangle, collision);
                            }
                            middlegroundElements.Add(mapElement);
                        }
                        else
                        {
                            if (layerObject.AnimationSequence != null)
                            {
                                ElementAnimation animation = GetAnimation(layerObject);
                                mapElement = new MapElement(
                                                            layerObject.Source.Value,
                                                            layerObject.DestinationRectangle,
                                                            animation);
                            }
                            else
                            {
                                mapElement = new MapElement(layerObject.Source.Value,
                                                            layerObject.DestinationRectangle);
                            }
                            mapElement = new MapElement(layerObject.Source.Value,
                                                        layerObject.DestinationRectangle);
                            middlegroundElements.Add(mapElement);
                        }
                        break;

                    case LayerType.Transition:
                        Rectangle rectangle = new Rectangle(layerObject.X, layerObject.Y,
                                                                layerObject.Width, layerObject.Height);
                        collision = new TransitionCollision(layerObject.ToId.Value, layerObject.ToMap, rectangle);
                        collisions.Add(collision);
                        break;
                }
            }
        }

        public ElementAnimation GetAnimation(LayerObject layerObject)
        {
            ElementAnimation animation = new ElementAnimation();
            Vector2 position = new Vector2(layerObject.DestinationRectangle.X, layerObject.DestinationRectangle.Y);
            animation.LoadContent(layerObject.AnimationSequence, 
                                  layerObject.DestinationRectangle, 
                                  layerObject.Width, 
                                  layerObject.Height,
                                  layerObject.AnimationRandomness.Value);
            return animation;
        }

        public void CollisionCheck()
        {
            foreach (MapCollision collision in collisions)
            {
                collision.Check(player, this);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (MapElement bg in backgroundElements)
            {
                bg.Update(gameTime);
            }

            foreach (MapElement mg in middlegroundElements)
            {
                mg.Update(gameTime);
            }

            foreach (MapElement fg in frontgroundElements)
            {
                fg.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MapElement bg in backgroundElements)
            {
                bg.Draw(spriteBatch);
            }

            var splitMiddleground = middlegroundElements.OrderBy(y => y.Destination.Bottom).GroupBy(
                x => (player.VerticalPosition + player.Height) > x.Destination.Bottom
                ).ToList();

            foreach (MapElement first in splitMiddleground.First())
            {
                first.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            foreach (MapElement last in splitMiddleground.Last())
            {
                last.Draw(spriteBatch);
            }

            foreach (MapElement fg in frontgroundElements)
            {
                fg.Draw(spriteBatch);
            }
        }

        //public void CollisionCheck(List<Rectangle> rectangle)
        //{
        //    Rectangle playerCollisionRectangle = player.CollisionRectangle;

        //    foreach (Rectangle r in rectangle)
        //    {
        //        if (r.Intersects(playerCollisionRectangle))
        //        {
        //            break;
        //        }
        //    }
        //}

        //public void CollisionCheck(List<CollidableMapElement> mapElement)
        //{
        //    Rectangle playerCollisionRectangle = player.CollisionRectangle;
        //}

        //public void UpdatePlayerAfterMovement()
        //{
        //    Rectangle playerCollisionRectangle = player.CollisionRectangle;

        //    foreach (Rectangle r in pathBlocks)
        //    {
        //        if (r.Intersects(playerCollisionRectangle))
        //        {


        //            break;
        //        }
        //    }
        //}
    }
}
