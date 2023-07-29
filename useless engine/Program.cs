using useless_engine.core;

const string _title = "useless engine";
const int _width = 64;
const int _height = 36;
const int _fps = 10;

Window window = new(_title, _width, _height, _fps);

window.OnRender = (renderer) =>
{
    renderer.Clear();

    renderer.DrawLine(
        -0.5f, -0.5f, 0.0f, 0.5f
    );

    renderer.DrawLine(
        0.0f, 0.5f, 0.5f, -0.5f
    );

    renderer.DrawLine(
        0.5f, -0.5f, -0.5f, -0.5f
    );
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