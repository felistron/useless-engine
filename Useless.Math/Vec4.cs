namespace Useless.Math
{
    public class Vec4<T>
    {
        private readonly T[] _data;
        public T X { get {  return _data[0]; } set { _data[0] = value; } }
        public T Y { get { return _data[1]; } set { _data[1] = value; } }
        public T Z { get {  return _data[2]; } set { _data[2] = value; } }
        public T W { get { return _data[3]; } set { _data[3] = value; } }

        public Vec4(T x, T y, T z, T w)
        {
            _data = new T[4] { x, y, z, w };
        }

        public T this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }
    }

    public class Vec4i : Vec4<int>
    {
        public static Vec4i Zero { get { return new Vec4i(0, 0, 0, 0); } }

        public Vec4i(int x, int y, int z, int w) : base(x, y, z, w) { }
    }

    public class Vec4f : Vec4<float>
    {
        public static Vec4f Zero { get { return new Vec4f(0, 0, 0, 0); } }

        public Vec4f(float x, float y, float z, float w) : base(x, y, z, w) { }
    }
}
