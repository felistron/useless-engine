using useless_engine.core;
using useless_engine.math;

const string _title = "useless engine";
const int _width = 128;
const int _height = 72;
const int _fps = 30;

Window window = new(_title, _width, _height, _fps);


Vec3f[] vertices = new Vec3f[3]
{
    new Vec3f(-0.5f, -0.5f, 0),
    new Vec3f( 0.0f,  0.5f, 0),
    new Vec3f( 0.5f, -0.5f, 0),
};

uint[] indices = new uint[4]
{
    0, 1, 2, 0
};

double scale = 0;

window.OnRender = (renderer) =>
{
    renderer.Clear();

    scale = Math.Sin(window.TimeAlive);

    Matx4 transform = Matx4.Identity() * Matx4.RotateX(window.TimeAlive) * Matx4.RotateY(window.TimeAlive) * Matx4.RotateZ(window.TimeAlive);

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
