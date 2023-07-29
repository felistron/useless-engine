namespace useless_engine.core
{
    public class Vector2<T>
    {
        public T X { get; set; }
        public T Y { get; set; }

        public Vector2(T x, T y)
        {
            X = x;
            Y = y;
        }
    }

    public class Vector2i : Vector2<int>
    {
        public Vector2i(int x, int y) : base(x, y) { }
        public Vector2i() : base(0, 0) { }
    }

    public class Vector2f : Vector2<float>
    {
        public Vector2f(float x, float y) : base(x, y) { }
        public Vector2f() : base(0, 0) { }
    }
}
