namespace useless_engine.math
{
    public class Vec2<T>
    {
        private readonly T[] _data;
        public T X { get { return _data[0]; } set { _data[0] = value; } }
        public T Y { get { return _data[1]; } set { _data[1] = value; } }

        public Vec2(T x, T y)
        {
            _data = new T[2] { x, y };
        }

        public T this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }
    }

    public class Vec2i : Vec2<int>
    {
        public static Vec2i Zero { get { return new Vec2i(0, 0); } }

        public Vec2i(int x, int y) : base(x, y) { }
    }

    public class Vec2f : Vec2<float>
    {
        public static Vec2f Zero { get { return new Vec2f(0, 0); } }

        public Vec2f(float x, float y) : base(x, y) { }
    }
}
