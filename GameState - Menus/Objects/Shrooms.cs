using Microsoft.Xna.Framework;

namespace CS5410.Objects
{
    public class Shrooms : AnimatedSprite
    {
        private int damage { get; set; } = 0;
        public Shrooms(Vector2 size, Vector2 center) : base(size, center)
        { }

    }
}
