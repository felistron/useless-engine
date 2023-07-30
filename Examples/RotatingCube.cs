using Useless.Core;
using Useless.Math;

const string _title = "useless engine";
const int _width = 128;
const int _height = 72;
const int _fps = 30;

Window window = new(_title, _width, _height, _fps);


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

window.OnRender = (renderer) =>
{
    renderer.Clear();

    Matx4 transform = Matx4.Identity()
                        * Matx4.RotateX(window.TimeAlive)
                        * Matx4.RotateY(window.TimeAlive)
                        * Matx4.RotateZ(window.TimeAlive);

    for (int i = 0; i < indices.Length - 1; i++)
    {
        Vec4f p1 = new(vertices[indices[i]].X, vertices[indices[i]].Y, vertices[indices[i]].Z, 1);
        Vec4f p2 = new(vertices[indices[i + 1]].X, vertices[indices[i + 1]].Y, vertices[indices[i + 1]].Z, 1);

        p1 = transform * p1;
        p2 = transform * p2;

        Line line = new(p1.X, p1.Y, p2.X, p2.Y);
        renderer.DrawLine(line);
    }
};

window.OnKeyEvent = (key) =>
{
    switch (key)
    {
        case ConsoleKey.Escape:
            window.Close();
            break;
    }
};

window.Run();
