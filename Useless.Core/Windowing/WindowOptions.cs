namespace Useless.Core.Windowing
{
    public class WindowOptions
    {
        public string Title { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public int Fps { get; set; }
        public bool VSync { get; set; }
        public double TimeAlive { get; set; }
        public bool CursorVisible { get; set; }
        public bool ShouldClose { get; set; }

        public WindowOptions()
        {
            Title = "Useless engine";
            Width = 800;
            Height = 600;
            Fps = 60;
            VSync = true;
            TimeAlive = 0;
            CursorVisible = true;
            ShouldClose = false;
        }
    }
}
