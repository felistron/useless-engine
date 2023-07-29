using useless_engine.core;

const string _title = "useless engine";
const int _width = 50;
const int _height = 20;
const int _fps = 15;

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