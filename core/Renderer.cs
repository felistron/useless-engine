using useless_engine.math;

namespace useless_engine.core
{
    public class Renderer
    {
        public int Width { get; }
        public int Height { get; }
        public uint[,] Buffer { get; private set; }

        private readonly float _hStep;
        private readonly float _vStep;

        public Renderer(int width, int height)
        {
            Width = width;
            Height = height;
            Buffer = new uint[width + 1, height + 1];
            _hStep = 1 / (float)width;
            _vStep = 1 / (float)height;
        }

        public void DrawPoint(Vec2f point)
        {
            DrawPoint(point.X, point.Y);
        }

        public void DrawPoint(float x, float y)
        {
            if (Math.Abs(x) > 1) return;
            if (Math.Abs(y) > 1) return;

            // Transforms (x,y) NDC into screen coordinates, defined by width and height.
            int charX = (int)((x + 1) * Width * 0.5f);
            int charY = (int)((y + 1) * Height * 0.5f);

            if (charX > Width || charX < 0 || charY > Height || charY < 0) return;
            Buffer[charX, charY] = 1;
        }


        public void DrawLine(Line line)
        {
            DrawLine(line.P1.X, line.P1.Y, line.P2.X, line.P2.Y);
        }

        public void DrawLine(Vec2f p1, Vec2f p2)
        {
            DrawLine(p1.X, p2.Y, p2.X, p2.Y);
        }

        public void DrawLine(float x1, float y1, float x2, float y2)
        {
            // p1 and p2 are in the same position so we draw a point
            if (x1 == x2 && y1 == y2)
            {
                DrawPoint(x1, y1);
                return;
            }

            // Vertical line
            if (x1 == x2)
            {
                float dt = (y2 - y1) > 0 ? _vStep : -_vStep;
                for (float y = y1; (dt * (y2 - y)) >= 0; y += dt)
                {
                    DrawPoint(x1, y);
                }
                return;
            }

            // Horizontal line
            if (y1 == y2)
            {
                float dt = (x2 - x1) > 0 ? _hStep : -_hStep;
                for (float x = x1; (dt * (x2 - x)) >= 0; x += dt)
                {
                    DrawPoint(x, y1);
                }
                return;
            }

            // Every other case
            float dx = (x2 - x1) > 0 ? _hStep : -_hStep;
            float m = (float)(y2 - y1) / (float)(x2 - x1);

            for (float x = x1; (dx * (x2 - x)) >= 0; x += dx)
            {
                DrawPoint(x,  (m * (x - x1)) + y1);
            }
        }


        public void Clear()
        {
            Buffer = new uint[Width + 1, Height + 1];
        }
    }
}
