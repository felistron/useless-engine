namespace Useless.Core.Windowing
{
    public class WindowOptions
    {
        public string Title { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public int Fps { get; set; }
        public bool VSync { get; set; }
        public bool CursorVisible { get; set; }

        public WindowOptions()
        {
            Title = "Useless engine";
            Width = 800;
            Height = 600;
            Fps = 60;
            VSync = true;
            CursorVisible = true;
        }
    }
}
