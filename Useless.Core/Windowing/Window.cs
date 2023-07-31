namespace Useless.Core.Windowing
{
    public static class Window
    {
        public static IWindow CreateConsoleWindow(WindowOptions options)
        {
            return new ConsoleWindow(options);
        }
    }
}
