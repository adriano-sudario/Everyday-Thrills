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
            name = model.Name;
            atlas = Loader.LoadTexture("mapAtlas");
            ////List<int[]> collisionArray = (List<int[]>)Utilities.LoadDeserializedJsonFile<object>(model.dataArrayFileName);
            //collisionRectangles = GetRectanglesByArray(model.dataArray, 1);
        }

        public void SetElements(MapModel model)
        {
            foreach(Layer layer in model.Layers)
            {
                AddElement(layer.Objects, layer.Type);
            }
        }

        public void AddElement(LayerObject[] layerObjects, LayerType type)
        {
            foreach (LayerObject layerObject in layerObjects)
            {
                MapCollision collision = null;
                MapElement mapElement = null;
                Rectangle destinationRectangle = new Rectangle(layerObject.X, layerObject.Y,
                                                               layerObject.Width, layerObject.Height);

                switch (type)
                {
                    case LayerType.Background:
                        mapElement = new MapElement(layerObject.Source.Value, destinationRectangle);
                        backgroundElements.Add(mapElement);
                        break;

                    case LayerType.Parallax:

                        break;

                    case LayerType.PathBlock:
                        collision = new PathBlockCollision(destinationRectangle);
                        collisions.Add(collision);
                        break;

                    case LayerType.SceneryElements:
                        if (layerObject.Collision != null)
                        {
                            collision = new PathBlockCollision(layerObject.Collision.Value);
                            collisions.Add(collision);
                            if (layerObject.AnimationSequence != null)
                            {
                                ElementAnimation animation = new ElementAnimation();
                                mapElement = new MapElement(layerObject.Source.Value, destinationRectangle, 
                                                            animation, collision);
                            }
                            else
                            {
                                mapElement = new MapElement(layerObject.Source.Value, destinationRectangle, collision);
                            }
                            middlegroundElements.Add(mapElement);
                        }
                        else
                        {
                            if (layerObject.AnimationSequence != null)
                            {
                                ElementAnimation animation = new ElementAnimation();
                                mapElement = new MapElement(layerObject.Source.Value, destinationRectangle,
                                                            animation);
                            }
                            else
                            {
                                mapElement = new MapElement(layerObject.Source.Value, destinationRectangle);
                            }
                            mapElement = new MapElement(layerObject.Source.Value, destinationRectangle);
                            backgroundElements.Add(mapElement);
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
