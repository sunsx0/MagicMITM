using System;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class Point3F : DataSerializer
    {
        public float X;
        public float Z;
        public float Y;

        public Point3F() { }
        public Point3F(Point3F point) : this(point.X, point.Z, point.Y)
        {
        }
        public Point3F(float x, float z, float y)
        {
            X = x;
            Z = z;
            Y = y;
        }

        public static Point3F operator +(Point3F p1, Point3F p2)
        {
            return new Point3F(p1.X + p2.X, p1.Z + p2.Z, p1.Y + p2.Y);
            
        }

        public static Point3F operator +(Point3F p1, float summand)
        {
            return new Point3F(p1.X + summand, p1.Z + summand, p1.Y + summand);
        }

        public static Point3F operator +(Point3F p1, double summand)
        {
            float summandF = (float)summand;
            return new Point3F(p1.X + summandF, p1.Z + summandF, p1.Y + summandF);
        }

        public static Point3F operator -(Point3F p1, Point3F p2)
        {
            return new Point3F(p1.X - p2.X, p1.Z - p2.Z, p1.Y - p2.Y);
        }

        public static Point3F operator -(Point3F p1, float subtrahend)
        {
            return new Point3F(p1.X - subtrahend, p1.Z - subtrahend, p1.Y - subtrahend);
        }

        public static Point3F operator -(Point3F p1, double subtrahend)
        {
            float subtrahendF = (float)subtrahend;
            return new Point3F(p1.X - subtrahendF, p1.Z - subtrahendF, p1.Y - subtrahendF);
        }

        public static Point3F operator *(Point3F p1, float multiplier)
        {
            return new Point3F(p1.X * multiplier, p1.Z * multiplier, p1.Y * multiplier);
        }

        public static Point3F operator /(Point3F p1, float divider)
        {
            return new Point3F(p1.X / divider, p1.Z / divider, p1.Y / divider);
        }

        public float Distance2D(Point3F destPoint)
        {
            double dx = destPoint.X - X;
            double dy = destPoint.Y - Y;

            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public float Distance3D(Point3F destPoint)
        {
            double dx = destPoint.X - X;
            double dy = destPoint.Y - Y;
            double dz = destPoint.Z - Z;

            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public override string ToString()
        {
            return String.Format("X: {0:0.00} Y: {1:0.00} Z: {2:0.00}", X, Y, Z);
        }

        public override DataStream Serialize(DataStream ds)
        {
            ds.
                Write(X).
                Write(Z).
                Write(Y);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            X = ds.ReadSingle();
            Z = ds.ReadSingle();
            Y = ds.ReadSingle();
            return base.Deserialize(ds);
        }
    }
}
