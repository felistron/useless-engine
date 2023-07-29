namespace useless_engine.core
{
    public class Line
    {
        public Vector2f P1 { get; set; }
        public Vector2f P2 { get; set; }

        public Line(Vector2f p1, Vector2f p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public Line(float x1, float y1, float x2, float y2)
        {
            P1 = new Vector2f(x1, y1);
            P2 = new Vector2f(x2, y2);
        }

        public Line()
        {
            P1 = new();
            P2 = new();
        }
    }
}
