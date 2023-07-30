using System.Text;

namespace useless_engine.math
{
    public class Matx4
    {
        private float[] _data = new float[16];

        public static Matx4 Identity()
        {
            return new Matx4()
            {
                _data = new float[16]
                {
                    1, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1,
                }
            };
        }

        public static Matx4 Scale(float s)
        {
            return new Matx4()
            {
                _data = new float[16]
                {
                    s, 0, 0, 0,
                    0, s, 0, 0,
                    0, 0, s, 0,
                    0, 0, 0, 1,
                }
            };
        }

        public static Matx4 Scale(float x, float y, float z)
        {
            return new Matx4()
            {
                _data = new float[16]
                {
                    x, 0, 0, 0,
                    0, y, 0, 0,
                    0, 0, z, 0,
                    0, 0, 0, 1,
                }
            };
        }

        public static Matx4 Translate(float x, float y, float z)
        {
            return new Matx4()
            {
                _data = new float[16]
                {
                    1, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    x, y, z, 1,
                }
            };
        }

        public static Matx4 Translate(Vec3f dest)
        {
            return Translate(dest.X, dest.Y, dest.Z);
        }

        public static Matx4 RotateX(double a)
        {
            float cos = (float)Math.Cos(a);
            float sin = (float)Math.Sin(a);

            return new Matx4()
            {
                _data = new float[16]
                {
                    1, 0  ,  0  , 0,
                    0, cos, -sin, 0,
                    0, sin,  cos, 0,
                    0, 0  ,  0  , 1,
                }
            };
        }

        public static Matx4 RotateY(double a)
        {
            float cos = (float)Math.Cos(a);
            float sin = (float)Math.Sin(a);

            return new Matx4()
            {
                _data = new float[16]
                {
                     cos, 0, sin, 0,
                     0  , 1,   0, 0,
                    -sin, 0, cos, 0,
                     0  , 0,   0, 1,
                }
            };
        }

        public static Matx4 RotateZ(double a)
        {
            float cos = (float)Math.Cos(a);
            float sin = (float)Math.Sin(a);

            return new Matx4()
            {
                _data = new float[16]
                {
                    cos, -sin, 0, 0,
                    sin,  cos, 0, 0,
                    0  ,  0  , 1, 0,
                    0  ,  0  , 0, 1,
                }
            };
        }

        public static Matx4 operator +(Matx4 a, Matx4 b)
        {
            float[] sum = new float[16];

            for (int i = 0; i < 16; i++)
            {
                sum[i] = a._data[i] + b._data[i];
            }

            return new Matx4()
            {
                _data = sum
            };
        }

        public static Matx4 operator -(Matx4 a, Matx4 b)
        {
            float[] sum = new float[16];

            for (int i = 0; i < 16; i++)
            {
                sum[i] = a._data[i] - b._data[i];
            }

            return new Matx4()
            {
                _data = sum
            };
        }

        public static Matx4 operator *(float scalar, Matx4 a)
        {
            float[] data = new float[16];

            for (int i = 0; i < 16; i++)
            {
                data[i] = scalar * a._data[i];
            }

            return new Matx4()
            {
                _data = data
            };
        }

        public static Matx4 operator *(Matx4 a, float scalar)
        {
            return scalar * a;
        }

        public static Matx4 operator *(Matx4 a, Matx4 b)
        {
            float[] c = new float[16];
            float res;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    res = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        res += a._data[j * 4 + k] * b._data[i * 4 + k];
                    }
                    c[i + (4 * j)] = res;
                }
            }

            return new Matx4()
            {
                _data = c
            };
        }

        public static Vec4f operator *(Matx4 a, Vec4f b)
        {
            Vec4f c = Vec4f.Zero;
            float res;

            for (int j = 0; j < 4; j++)
            {
                res = 0;

                for (int k = 0; k < 4; k++)
                {
                    res += a._data[j * 4 + k] * b[k];
                }

                c[j] = res;
            }

            return c;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            for (int i = 0; i < _data.Length; i++)
            {
                sb.Append(_data[i].ToString() + ' ');
                if (((i + 1) % 4) == 0) sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}
