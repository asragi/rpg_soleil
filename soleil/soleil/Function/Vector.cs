using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Soleil
{
    /// <summary>
    /// 位置、速度などを扱う構造体
    /// </summary>
    [Serializable]
    struct Vector
    {
        public double X;
        public double Y;
        const double Eps = 1.0e-10;
        public Vector(double px, double py)
        {
            X = px;
            Y = py;
        }
        public Vector(double p) : this(p, p) { }
        /// <summary>
        /// 零ベクトル
        /// </summary>
        public static Vector Zero => new Vector(0);
        /// <summary>
        /// 全ての要素が1のベクトル
        /// </summary>
        public static Vector One => new Vector(1);
        /// <summary>
        /// 角度と長さからVectorを得る
        /// </summary>
        /// <param name="angle">角度（度数法）</param>
        /// <param name="length">長さ</param>
        /// <returns></returns>
        public static Vector GetFromAngleLength(double angle, double length) =>
            new Vector(MathEx.Cos(angle), MathEx.Sin(angle)) * length;
        /// <summary>
        /// 長さを得る
        /// </summary>
        /// <returns></returns>
        public double GetLength() => Math.Pow(X * X + Y * Y, 0.5);
        /// <summary>
        /// 角度を得る
        /// </summary>
        /// <returns></returns>
        public double GetAngle() => MathEx.Atan2(Y, X);
        /// <summary>
        /// 長さの2乗を得る
        /// </summary>
        /// <returns></returns>
        public double GetLengthSquare() => X * X + Y * Y;
        /// <summary>
        /// 方向が同じ単位ベクトルを返す
        /// </summary>
        /// <returns></returns>
        public Vector GetUnit()
        {
            double leng = GetLength();
            return leng == 0 ? new Vector(1, 0) : (this / leng);
        }
        /// <summary>
        /// 回転したベクトルを返す
        /// </summary>
        /// <param name="a">回転角（度数法）</param>
        /// <returns></returns>
        public Vector Rotate(double a)
        {
            double cos = MathEx.Cos(a);
            double sin = MathEx.Sin(a);
            return new Vector(X * cos - Y * sin, X * sin + Y * cos);
        }
        /// <summary>
        /// valueをminとmaxで作れる長方形に収まるようにする
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static Vector Clamp(Vector value, Vector max, Vector min)
        {
            return new Vector(MathEx.Clamp(value.X, max.X, min.X), MathEx.Clamp(value.Y, max.Y, min.Y));
        }
        /// <summary>
        /// fromからtoへの角度を取得する（度数法）
        /// </summary>
        public static double GetAngle(Vector from, Vector to) => Math.Atan2(to.Y - from.Y, to.X - from.X) * 180 / Math.PI;
        /// <summary>
        /// fromからtoへのlengthのベクトルを取得する
        /// </summary>
        public static Vector GetAdjustedVector(Vector from, Vector to, double length) => (to - from).GetUnit() * length;
        public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector operator -(Vector v1, Vector v2) => new Vector(v1.X - v2.X, v1.Y - v2.Y);
        public static double operator *(Vector v1, Vector v2) => (v1.X * v2.X + v1.Y * v2.Y);
        public static Vector operator *(Vector v1, double k) => new Vector(v1.X * k, v1.Y * k);
        public static Vector operator *(double k, Vector v1) => new Vector(v1.X * k, v1.Y * k);
        public static Vector operator /(Vector v1, double k) => new Vector(v1.X / k, v1.Y / k);
        public static bool operator ==(Vector v1, Vector v2) => (Math.Abs(v1.X - v2.X) < Eps && Math.Abs(v1.Y - v2.Y) < Eps);
        public static bool operator !=(Vector v1, Vector v2) => !(v1 == v2);
        public static Vector operator -(Vector v1) => v1 * (-1);
        //implicitとexplicitは本来は意味的に逆にすべきだが楽をするための手抜き
        //（Vector→Vector2は情報が落ちるが、Vector2→Vectorは情報が落ちない）
        public static implicit operator Vector2(Vector v) => new Vector2((float)(v.X/* + 0.5*/), (float)(v.Y/* + 0.5*/));
        public static explicit operator Vector(Vector2 v) => new Vector(v.X, v.Y);
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) return false;
            Vector v = (Vector)obj;
            return (Math.Abs(X - v.X) < Eps && Math.Abs(Y - v.Y) < Eps);
        }
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
        public override string ToString() => $"({X}, {Y})";
        /// <summary>
        /// 小数点以下の桁数を指定して、決められた文字数で文字列に
        /// </summary>
        /// <param name="l">桁数</param>
        /// <param name="num">文字数</param>
        /// <returns></returns>
        public string ToString(int l, int num) =>
            "(" + string.Format("{0:f" + l + "}", X).PadLeft(num) + "," + string.Format("{0:f" + l + "}", Y).PadLeft(num) + ")";


        public Vector XAxialInversion() => new Vector(X, -Y);
        public Vector YAxialInversion() => new Vector(-X, Y);

    }
}
