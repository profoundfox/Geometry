using System;
using Microsoft.Xna.Framework;

namespace Geometry
{
    public struct RectangleShape2D : IShape2D
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public Vector2[] Vertices
        {
            get
            {
                return new Vector2[]
                {
                    new Vector2(0, 0),                     
                    new Vector2(Width, 0),                 
                    new Vector2(Width, Height),
                    new Vector2(0, Height)
                };
            }
        }

        public RectangleShape2D(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public bool Intersect(IShape2D otherShape, Vector2 thisPosition, Vector2 otherPosition)
        {
            if (otherShape is RectangleShape2D)
            {
                var otherRect = (RectangleShape2D)otherShape;
                return AABBIntersect(thisPosition, this, otherPosition, otherRect);
            }

            if (otherShape is CircleShape2D)
            {
                var otherCircle = (CircleShape2D)otherShape;
                return RectangleIntersectWithCircle(thisPosition, this, otherPosition, otherCircle);
            }

            return false;
        }

        private bool AABBIntersect(Vector2 pos1, RectangleShape2D rect1, Vector2 pos2, RectangleShape2D rect2)
        {
            var min1 = pos1;
            var max1 = pos1 + new Vector2(rect1.Width, rect1.Height);

            var min2 = pos2;
            var max2 = pos2 + new Vector2(rect2.Width, rect2.Height);

            return (min1.X <= max2.X && max1.X >= min2.X &&
                    min1.Y <= max2.Y && max1.Y >= min2.Y);
        }

        public bool RectangleIntersectWithCircle(Vector2 rectPos, RectangleShape2D rect, Vector2 circlePos, CircleShape2D circle)
        {
            float closestX = Math.Clamp(circlePos.X, rectPos.X, rectPos.X + rect.Width);
            float closestY = Math.Clamp(circlePos.Y, rectPos.Y, rectPos.Y + rect.Height);

            float distanceX = circlePos.X - closestX;
            float distanceY = circlePos.Y - closestY;
            float distanceSquared = distanceX * distanceX + distanceY * distanceY;

            return distanceSquared <= circle.Radius * circle.Radius;
        }
    }
}