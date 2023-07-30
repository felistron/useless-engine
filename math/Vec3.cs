namespace useless_engine.math
{
    public class Vec3<T>
    {
        private readonly T[] _data;
        public T X { get { return _data[0]; } set { _data[0] = value; } }
        public T Y { get { return _data[1]; } set { _data[1] = value; } }
        public T Z { get { return _data[2]; } set { _data[2] = value; } }

        public Vec3(T x, T y, T z)
        {
            _data = new T[3] { x, y, z };
        }

        public T this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }
    }

    public class Vec3i : Vec3<int>
    {
        public static Vec3i Zero { get { return new Vec3i(0, 0, 0); } }

        public Vec3i(int x, int y, int z) : base(x, y, z) { }
    }

    public class Vec3f : Vec3<float>
    {
        public static Vec3f Zero { get { return new Vec3f(0, 0, 0); } }

        public Vec3f(float x, float y, float z) : base(x, y, z) { }
    }
}
