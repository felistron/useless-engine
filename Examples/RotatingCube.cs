using Useless.Core.Graphics;
using Useless.Core.Input;
using Useless.Core.Windowing;
using Useless.Math;

const string _title = "useless engine - Rotating Cube";
const int _width = 40;
const int _height = 40;
const int _fps = 30;

WindowOptions options = new()
{
    Title = _title,
    Width = _width,
    Height = _height,
    Fps = _fps,
};

IWindow window = Window.CreateConsoleWindow(options);


Vec3f[] vertices = new Vec3f[8]
{
    new Vec3f(-0.5f, -0.5f, -0.5f),
    new Vec3f(-0.5f,  0.5f, -0.5f),
    new Vec3f( 0.5f,  0.5f, -0.5f),
    new Vec3f( 0.5f, -0.5f, -0.5f),

    new Vec3f(-0.5f, -0.5f,  0.5f),
    new Vec3f(-0.5f,  0.5f,  0.5f),
    new Vec3f( 0.5f,  0.5f,  0.5f),
    new Vec3f( 0.5f, -0.5f,  0.5f),
};

uint[] indices = new uint[16]
{
    0, 1, 5, 1, 2, 6, 2, 3, 7, 3, 0, 4, 7, 6, 5, 4
};

long nearColor  = 0xFFA1A1A1;
long farColor   = 0xFF101010;
Matx4 transform;
long color1, color2;
Line line;
Vec4f p1, p2; 

window.OnRender = (renderer) =>
{
    renderer.Clear();

    transform = Matx4.Identity()
                * Matx4.RotateX(window.TimeAlive)
                * Matx4.RotateY(window.TimeAlive)
                * Matx4.RotateZ(window.TimeAlive);

    for (int i = 0; i < indices.Length - 1; i++)
    {
        p1 = new(vertices[indices[i]].X, vertices[indices[i]].Y, vertices[indices[i]].Z, 1);
        p2 = new(vertices[indices[i + 1]].X, vertices[indices[i + 1]].Y, vertices[indices[i + 1]].Z, 1);

        p1 = transform * p1;
        p2 = transform * p2;

        color1 = nearColor;
        color2 = nearColor;

        if (p1.Z < 0) color1 = farColor;
        if (p2.Z < 0) color2 = farColor;

        line = new(p1.X, p1.Y, p2.X, p2.Y);
        renderer.DrawLine(line, color1, color2);
    }
};

window.OnKeyPress = (key) =>
{
    switch (key)
    {
        case Key.Esc:
            window.Close();
            break;
    }
};

window.Run();
