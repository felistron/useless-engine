using useless_engine.math;

namespace useless_engine.core
{
    public class Line
    {
        public Vec2f P1 { get; set; }
        public Vec2f P2 { get; set; }

        public Line(Vec2f p1, Vec2f p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public Line(float x1, float y1, float x2, float y2)
        {
            P1 = new Vec2f(x1, y1);
            P2 = new Vec2f(x2, y2);
        }

        public Line()
        {
            P1 = Vec2f.Zero;
            P2 = Vec2f.Zero;
        }
    }
}
