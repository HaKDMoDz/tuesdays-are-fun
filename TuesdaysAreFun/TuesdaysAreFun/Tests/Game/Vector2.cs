using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TuesdaysAreFun.Tests.Game
{
	// Class for mutability
	public class Vector2 : IEquatable<Vector2>
	{
		public static Vector2 Zero
		{
			get
			{
				return new Vector2(0, 0);
			}
		}
		public static Vector2 One
		{
			get
			{
				return new Vector2(1, 1);
			}
		}
		public static Vector2 UnitX
		{
			get
			{
				return new Vector2(1, 0);
			}
		}
		public static Vector2 UnitY
		{
			get
			{
				return new Vector2(0, 1);
			}
		}

		public double X
		{
			get
			{
				return _x;
			}
			set
			{
				OnXChanging(_x);
				_x = value;
				OnXChanged(_x);
			}
		}
		double _x;

		public double Y
		{
			get
			{
				return _y;
			}
			set
			{
				OnYChanging(_y);
				_y = value;
				OnYChanged(_y);
			}
		}
		double _y;

		public event EventHandler<double> XChanging;
		public event EventHandler<double> XChanged;
		public event EventHandler<double> YChanging;
		public event EventHandler<double> YChanged;

		public Vector2(double x, double y)
		{
			_x = x;
			_y = y;
		}
		public Vector2(Point p) : this(p.X, p.Y)
		{ }

		private void OnXChanging(double oldVal)
		{
			if (XChanging != null)
			{
				XChanging(this, oldVal);
			}
		}
		private void OnXChanged(double newVal)
		{
			if (XChanged != null)
			{
				XChanged(this, newVal);
			}
		}

		private void OnYChanging(double oldVal)
		{
			if (YChanging != null)
			{
				YChanging(this, oldVal);
			}
		}
		private void OnYChanged(double newVal)
		{
			if (YChanged != null)
			{
				YChanged(this, newVal);
			}
		}

		public static double DotProduct(Vector2 l, Vector2 r)
		{
			return (l.X * r.X) + (l.Y * r.Y);
		}
		public double DotProduct(Vector2 other)
		{
			return Vector2.DotProduct(this, other);
		}

		public double Length()
		{
			return Math.Sqrt(X * X + Y * Y);
		}
		public static Vector2 Negate(Vector2 v)
		{
			return new Vector2(-v.X, -v.Y);
		}

		public override bool Equals(object obj)
		{
			Vector2 v0 = obj as Vector2;
			if (v0 == null)
			{
				return false;
			}

			return Equals(v0);
		}
		public bool Equals(Vector2 other)
		{
			if (other == null)
			{
				return false;
			}

			return X == other.X && Y == other.Y;
		}

		public override int GetHashCode()
		{
			return (X.GetHashCode() << 16) + Y.GetHashCode();
		}

		public override string ToString()
		{
			return "(" + X.ToString() + ", " + Y.ToString() + ")";
		}

		public static bool operator==(Vector2 l, Vector2 r)
		{
			if (l as object == null && r as object == null)
			{
				return true;
			}
			else if (l as object == null)
			{
				return false;
			}

			return l.Equals(r);
		}
		public static bool operator!=(Vector2 l, Vector2 r)
		{
			if (l as object == null && r as object == null)
			{
				return false;
			}
			else if (l as object == null)
			{
				return true;
			}

			return !l.Equals(r);
		}

		public static Vector2 operator+(Vector2 l, Vector2 r)
		{
			return new Vector2(l.X + r.X, l.Y + r.Y);
		}
		public static Vector2 operator-(Vector2 l, Vector2 r)
		{
			return new Vector2(l.X - r.X, l.Y - r.Y);
		}
		public static Vector2 operator*(Vector2 v, float k)
		{
			return new Vector2(v.X * k, v.Y * k);
		}
		public static Vector2 operator/(Vector2 v, float k)
		{
			return new Vector2(v.X / k, v.Y / k);
		}

		public static Vector2 operator-(Vector2 neg)
		{
			return Negate(neg);
		}
	}
}
