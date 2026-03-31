using Microsoft.Xna.Framework;

namespace Geometry
{   public interface IShape2D
    {
        Vector2[] Vertices { get; }
        bool Intersect(IShape2D otherShape, Vector2 thisPosition, Vector2 otherPosition);
    }
}