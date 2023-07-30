using Useless.Math;

namespace Useless.Core.Graphics
{
    public class Renderer
    {
        public uint Width { get; }
        public uint Height { get; }
        public long[,] Buffer { get; private set; }

        private readonly float _hStep;
        private readonly float _vStep;

        private readonly long _redMask   = 0x00FF0000;
        private readonly long _greenMask = 0x0000FF00;
        private readonly long _blueMask  = 0x000000FF;

        public Renderer(uint width, uint height)
        {
            Width = width;
            Height = height;
            Buffer = new long[width + 1, height + 1];
            _hStep = 1 / (float)width;
            _vStep = 1 / (float)height;
        }

        public void DrawPoint(Vec2f point, long color)
        {
            DrawPoint(point.X, point.Y, color);
        }

        public void DrawPoint(float x, float y, long color)
        {
            if (System.Math.Abs(x) > 1) return;
            if (System.Math.Abs(y) > 1) return;

            // Transforms (x,y) NDC into screen coordinates, defined by width and height.
            int charX = (int)((x + 1) * Width * 0.5f);
            int charY = (int)((y + 1) * Height * 0.5f);

            if (charX > Width || charX < 0 || charY > Height || charY < 0) return;
            Buffer[charX, charY] = color;
        }


        public void DrawLine(Line line, long color1, long color2)
        {
            DrawLine(line.P1.X, line.P1.Y, line.P2.X, line.P2.Y, color1, color2);
        }

        public void DrawLine(Vec2f p1, Vec2f p2, long color1, long color2)
        {
            DrawLine(p1.X, p2.Y, p2.X, p2.Y, color1, color2);
        }

        public void DrawLine(float x1, float y1, float x2, float y2, long color1, long color2)
        {
            // p1 and p2 are in the same position so we draw a point
            if (x1 == x2 && y1 == y2)
            {
                // TODO: Combine two colors into one
                DrawPoint(x1, y1, color1);
                return;
            }

            double mixLevel;
            long color;
            long r, g, b;

            // Vertical line
            if (x1 == x2)
            {
                float dt = y2 - y1 > 0 ? _vStep : -_vStep;
                for (float y = y1; dt * (y2 - y) >= 0; y += dt)
                {
                    mixLevel = (dt * (y2 - y)) / (dt * (y2 - y1));

                    r = (long)(((color2 & _redMask  ) - (color1 & _redMask  )) * mixLevel) + (color1 & _redMask  ) & _redMask;
                    g = (long)(((color2 & _greenMask) - (color1 & _greenMask)) * mixLevel) + (color1 & _greenMask) & _greenMask;
                    b = (long)(((color2 & _blueMask ) - (color1 & _blueMask )) * mixLevel) + (color1 & _blueMask ) & _blueMask;

                    color = 0xFF000000 + r + g + b;

                    DrawPoint(x1, y, color);
                }
                return;
            }

            // Horizontal line
            if (y1 == y2)
            {
                float dt = x2 - x1 > 0 ? _hStep : -_hStep;
                for (float x = x1; dt * (x2 - x) >= 0; x += dt)
                {
                    mixLevel = (dt * (x2 - x)) / (dt * (x2 - x1));

                    r = (long)(((color2 & _redMask  ) - (color1 & _redMask  )) * mixLevel) + (color1 & _redMask  ) & _redMask;
                    g = (long)(((color2 & _greenMask) - (color1 & _greenMask)) * mixLevel) + (color1 & _greenMask) & _greenMask;
                    b = (long)(((color2 & _blueMask ) - (color1 & _blueMask )) * mixLevel) + (color1 & _blueMask ) & _blueMask;

                    color = 0xFF000000 + r + g + b;

                    DrawPoint(x, y1, color);
                }
                return;
            }

            // Every other case
            float dx = x2 - x1 > 0 ? _hStep : -_hStep;
            float m = (float)(y2 - y1) / (float)(x2 - x1);

            for (float x = x1; dx * (x2 - x) >= 0; x += dx)
            {
                mixLevel = (dx * (x2 - x)) / (dx * (x2 - x1));

                r = (long)(((color2 & _redMask  ) - (color1 & _redMask  )) * mixLevel) + (color1 & _redMask  ) & _redMask;
                g = (long)(((color2 & _greenMask) - (color1 & _greenMask)) * mixLevel) + (color1 & _greenMask) & _greenMask;
                b = (long)(((color2 & _blueMask ) - (color1 & _blueMask )) * mixLevel) + (color1 & _blueMask ) & _blueMask;

                color = 0xFF000000 + r + g + b;

                DrawPoint(x, m * (x - x1) + y1, color);
            }
        }


        public void Clear()
        {
            Buffer = new long[Width + 1, Height + 1];
        }
    }
}
