namespace Useless.Core
{
    public class Window
    {
        private static readonly char[] TILES = { ' ', '.' };

        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Fps { get; set; }
        public double TimeAlive { get; private set; }
        public bool ShouldClose { get; private set; } = false;
        public bool CursorVisible { get; set; } = false;
        public Action<double> OnUpdate { get; set; } = (_) => { };
        public Action<Renderer> OnRender { get; set; } = (_) => { };
        public Action<ConsoleKey> OnKeyEvent { get; set; } = (_) => { };

        private Renderer _renderer;

        private double _dt = 0;
        private long _oldTime = 0;
        private long _newTime = 0;

        public Window(string title, int width, int height, int fps)
        {
            Title = title;
            Width = width;
            Height = height;
            Fps = fps;

            _renderer = new(Width, Height);
            Console.Title = Title;
            Console.CursorVisible = CursorVisible;

            new Thread(() =>
            {
                while(!ShouldClose)
                {
                    OnKeyEvent(Console.ReadKey().Key);
                }
            }).Start();
        }

        public void Run()
        {
            while(!ShouldClose)
            {
                _newTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                _dt = ((double)_newTime - (double)_oldTime) / 1000;
                _oldTime = _newTime;

                TimeAlive += _dt;
                OnUpdate(_dt);
                OnRender(_renderer);

                Console.CursorTop = 0;
                Console.CursorLeft = 0;

                string data = string.Empty;

                for (int j = Height; j >= 0; j--)
                {
                    for (int i = 0; i <= Width; i++)
                    {
                        if (_renderer.Buffer[i, j] >= TILES.Length) continue;
                        data += TILES[_renderer.Buffer[i, j]];
                    }
                    data += '\n';
                }

                Console.CursorTop = 0;
                Console.CursorLeft = 0;

                Console.Write(data);

                Thread.Sleep(1000 / Fps);
            }
        }

        public void Close()
        {
            ShouldClose = true;
        }
    }
}
