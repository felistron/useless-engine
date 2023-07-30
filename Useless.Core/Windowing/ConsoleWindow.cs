using Useless.Core.Graphics;
using Useless.Core.Input;

namespace Useless.Core.Windowing
{
    public class ConsoleWindow : IWindow
    {
        private static readonly string charColors = " .\'`^\",:;Il!i><~+_-?][}{1)(|\\/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";


        public WindowOptions Options { get; }

        public Action OnLoad { get; set; } = () => { };
        
        public Action<Renderer> OnRender { get; set; } = (_) => { };
        
        public Action<double> OnUpdate { get; set; } = (_) => { };
        
        public Action<Key> OnKeyPress { get; set; } = (_) => { };

        public double TimeAlive { get; private set; }
        public bool ShouldClose { get; private set; }


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
                while (!ShouldClose)
                {
                    OnKeyPress((Key)Console.ReadKey().Key);
                }
            }).Start();

            OnLoad();
        }

        public void Run()
        {
            while (!ShouldClose)
            {
                _newTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                _dt = (_newTime - (double)_oldTime) / 1000;
                _oldTime = _newTime;

                TimeAlive += _dt;
                OnUpdate(_dt);
                OnRender(_renderer);

                Console.CursorTop = 0;
                Console.CursorLeft = 0;

                string data = string.Empty;

                for (int j = 0; j <= Options.Height; j++)
                {
                    for (int i = 0; i <= Options.Width; i++)
                    {
                        int color = (int)System.Math.Round((double)(_renderer.Buffer[i, j] & 0x00FFFFFF) / (double)(0xFFFFFF) * (charColors.Length - 1));
                        if (color >= charColors.Length) continue;
                        data += charColors[color];
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
            ShouldClose = true;
        }
    }
}
