using Microsoft.Xna.Framework;

namespace Geometry
{
    public struct CircleShape2D : IShape2D
    {
        public float Radius { get; set; }

        public Vector2[] Vertices => new Vector2[0];

        public CircleShape2D(float radius)
        {
            Radius = radius;
        }

        public bool Intersect(IShape2D otherShape, Vector2 thisPosition, Vector2 otherPosition)
        {
            if (otherShape is CircleShape2D)
            {
                var otherCircle = (CircleShape2D)otherShape;
                return CircleIntersect(thisPosition, this, otherPosition, otherCircle);
            }

            if (otherShape is RectangleShape2D)
            {
                var otherRect = (RectangleShape2D)otherShape;
                return RectangleIntersectWithCircle(thisPosition, this, otherPosition, otherRect);
            }

            return false;
        }

        private bool CircleIntersect(Vector2 pos1, CircleShape2D circle1, Vector2 pos2, CircleShape2D circle2)
        {
            float distance = Vector2.Distance(pos1, pos2);

            return distance <= (circle1.Radius + circle2.Radius);
        }

        private bool RectangleIntersectWithCircle(Vector2 circlePos, CircleShape2D circle, Vector2 rectPos, RectangleShape2D rect)
        {
            return rect.RectangleIntersectWithCircle(rectPos, rect, circlePos, circle);
        }
    }
}