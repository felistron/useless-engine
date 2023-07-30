using Useless.Core.Graphics;
using Useless.Core.Input;

namespace Useless.Core.Windowing
{
    public interface IWindow
    {
        public WindowOptions Options { get; }
        public Action OnLoad { get; set; }
        public Action<Renderer> OnRender { get; set; }
        public Action<double> OnUpdate { get; set; }
        public Action<Key> OnKeyPress { get; set; }

        public void Run();
        public void Close();
    }
}
