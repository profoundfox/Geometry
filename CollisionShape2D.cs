using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace Geometry
{
    public class CollisionNode2D
    {
        public IShape2D Shape { get; set; }
        public Vector2 Position { get; set; }

        public CollisionNode2D(Vector2 position, IShape2D shape)
        {
            Shape = shape;
            Position = position;
        }

        public bool CheckIntersection(CollisionNode2D other)
        {
            return Shape.Intersect(other.Shape, this.Position, other.Position);
        }

    }
}