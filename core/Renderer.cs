namespace useless_engine.core
{
    public class Renderer
    {
        private static readonly char PIXEL_CHAR = '.';
        private static readonly char EMPTY_CHAR = ' ';

        public int Width { get; }
        public int Height { get; }
        public char[,] Buffer { get; private set; }
        private char[,] _emptyBuffer;
        public TextWriter BufferWriter { get; private set; }

        public Renderer(int width, int height, TextWriter bufferWriter)
        {
            Width = width;
            Height = height;
            Buffer = new char[width + 1, height + 1];
            BufferWriter = bufferWriter;

            _emptyBuffer = new char[width + 1, height + 1];

            for (int j = 0; j <= Height; j++)
            {
                for (int i = 0; i <= Width; i++)
                {
                    _emptyBuffer[i, j] = EMPTY_CHAR;
                }
            }
        }

        public void DrawPoint(Vector2f point)
        {
            DrawPoint(point.X, point.Y);
        }

        public void DrawPoint(float x, float y)
        {
            if (Math.Abs(x) > 1) return;
            if (Math.Abs(y) > 1) return;

            int charX = (int)((x + 1) * Width / 2);
            int charY = (int)((y + 1) * Height/ 2);

            if (charX > Width || charX < 0 || charY > Height || charY < 0) return;

            Buffer[charX, charY] = PIXEL_CHAR;
        }


        public void DrawLine(Line line)
        {
            DrawLine(line.P1.X, line.P1.Y, line.P2.X, line.P2.Y);
        }

        public void DrawLine(Vector2f p1, Vector2f p2)
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

            float step = 0.01f;

            // Vertical line
            if (x1 == x2)
            {
                float dt = (y2 - y1) > 0 ? step : -step;
                for (float y = y1; (dt * (y2 - y)) >= 0; y += dt)
                {
                    DrawPoint(x1, y);
                }
                return;
            }

            // Horizontal line
            if (y1 == y2)
            {
                float dt = (x2 - x1) > 0 ? step : -step;
                for (float x = x1; (dt * (x2 - x)) >= 0; x += dt)
                {
                    DrawPoint(x, y1);
                }
                return;
            }

            // Every other case
            float dx = (x2 - x1) > 0 ? step : -step;
            float m = (float)(y2 - y1) / (float)(x2 - x1);

            for (float x = x1; (dx * (x2 - x)) >= 0; x += dx)
            {
                DrawPoint(x,  (m * (x - x1)) + y1);
            }
        }


        public void Clear()
        {
            Buffer = _emptyBuffer;
        }
    }
}
