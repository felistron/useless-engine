namespace useless_engine.core
{
    public class Vector3<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }

        public Vector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class Vector3i : Vector3<int>
    {
        public Vector3i(int x, int y, int z) : base(x, y, z) { }
        public Vector3i() : base(0, 0, 0) { }
    }

    public class Vector3f : Vector3<float>
    {
        public Vector3f(float x, float y, float z) : base(x, y, z) { }
        public Vector3f() : base(0, 0, 0) { }
    }
}
