using Useless.Core.Graphics;
using Useless.Core.Input;

namespace Useless.Core.Windowing
{
    public class ConsoleWindow : IWindow
    {
        private static readonly char[] TILES = { ' ', '.' };


        public WindowOptions Options { get; }

        public Action OnLoad { get; set; } = () => { };
        
        public Action<Renderer> OnRender { get; set; } = (_) => { };
        
        public Action<double> OnUpdate { get; set; } = (_) => { };
        
        public Action<Key> OnKeyPress { get; set; } = (_) => { };
        

        private readonly Renderer _renderer;

        private double _dt = 0;
        private long _oldTime = 0;
        private long _newTime = 0;

        public ConsoleWindow(WindowOptions options)
        {
            Options = options;

            _renderer = new Renderer(options.Width, options.Height);

            Console.Title = options.Title;
            Console.CursorVisible = options.CursorVisible;

            Load();
        }

        private void Load()
        {
            new Thread(() =>
            {
                while (!Options.ShouldClose)
                {
                    OnKeyPress((Key)Console.ReadKey().Key);
                }
            }).Start();

            OnLoad();
        }

        public void Run()
        {
            while (!Options.ShouldClose)
            {
                _newTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                _dt = (_newTime - (double)_oldTime) / 1000;
                _oldTime = _newTime;

                Options.TimeAlive += _dt;
                OnUpdate(_dt);
                OnRender(_renderer);

                Console.CursorTop = 0;
                Console.CursorLeft = 0;

                string data = string.Empty;

                for (int j = (int)Options.Height; j >= 0; j--)
                {
                    for (int i = 0; i <= Options.Width; i++)
                    {
                        if (_renderer.Buffer[i, j] >= TILES.Length) continue;
                        data += TILES[_renderer.Buffer[i, j]];
                    }
                    data += '\n';
                }

                Console.CursorTop = 0;
                Console.CursorLeft = 0;

                Console.Write(data);

                Thread.Sleep(1000 / Options.Fps);
            }
        }

        public void Close()
        {
            Options.ShouldClose = true;
        }
    }
}
