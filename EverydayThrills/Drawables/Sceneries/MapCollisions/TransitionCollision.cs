using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayThrills.Drawables.Sceneries.MapCollisions
{
    public class TransitionCollision : MapCollision
    {
        public int ToId;
        public string ToMap;

        public TransitionCollision(int toId, string toMap, Rectangle collisionRectangle) : base (collisionRectangle)
        {
            ToId = toId;
            ToMap = toMap;
            CollisionOccurred += TransitionCollision_CollisionOccurred;
        }

        private void TransitionCollision_CollisionOccurred(object sender, CollisionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
