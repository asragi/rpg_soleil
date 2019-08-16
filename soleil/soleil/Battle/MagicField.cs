using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    enum MagicFieldName
    {
        Magic,
        Sound,
        Light,
        Wood,
        Curse,
        Compassion,
        Shadow,
        Steel,
    }
    abstract class MagicField
    {
        public const int MagicFieldCount = 10;

        public MagicField()
        {
        }

        public abstract float GetMagicRate(MagicFieldName field);

        public abstract void Fluctuate(MagicFieldName field, float power);
    }

    class SimpleMagicField : MagicField
    {
        const float InitialMagicRate = 50f;
        const float MagicRateMax = 100f;
        const float MagicRateMin = 0f;
        float[] magicRates;

        public SimpleMagicField() : base()
        {
            magicRates = new float[MagicFieldCount];
            for (int i = 0; i < MagicFieldCount; i++)
                magicRates[i] = InitialMagicRate;
        }

        public void SetMagicRate(MagicFieldName field, float value)
        {
            //倍率2倍以上or0.5倍以下にはならない
            if (value > 1f)
                magicRates[(int)field] = value * 50;
            else
                magicRates[(int)field] = (value - 0.5f) * 100;
        }
        public override float GetMagicRate(MagicFieldName field)
        {
            if (magicRates[(int)field] > InitialMagicRate)
                return magicRates[(int)field] / 50;
            else
                return magicRates[(int)field] / 100 + 0.5f;
        }

        void MagicRateAdd(int field, float value)
        {
            magicRates[field] += value;
            if (magicRates[field] < MagicRateMin) magicRates[(int)field] = MagicRateMin;
            if (magicRates[field] > MagicRateMax) magicRates[(int)field] = MagicRateMax;
        }

        public override void Fluctuate(MagicFieldName field, float power)
        {
            MagicRateAdd((int)field, -20);
            MagicRateAdd((int)(field + 1) % MagicFieldCount, -10);
            MagicRateAdd((int)(field - 1) % MagicFieldCount, -10);
            MagicRateAdd((int)(field + 5) % MagicFieldCount, 20);
            MagicRateAdd((int)(field + 5 + 1) % MagicFieldCount, 10);
            MagicRateAdd((int)(field + 5 - 1) % MagicFieldCount, 10);
        }
    }

    class PhisicalMagicField : MagicField
    {
        //極は半径FieldRadius上の円上
        //中心座標は半径radiusの円上を動く

        float x, y;
        float[] fieldPosX, fieldPosY;
        const float FieldRadius = 2f;
        const float radius = 1f;
        public PhisicalMagicField() : base()
        {
            x = 0;
            y = 0;
            fieldPosX = new float[MagicFieldCount];
            fieldPosY = new float[MagicFieldCount];
            for (int i = 0; i < MagicFieldCount; i++)
            {
                fieldPosX[i] = (float)MathEx.Cos(i * (360f / MagicFieldCount) + 180) * FieldRadius;
                fieldPosY[i] = (float)MathEx.Sin(i * (360f / MagicFieldCount) + 180) * FieldRadius;
            }
        }

        public void SetMagicRate(MagicFieldName field, float value)
        {

        }
        public override float GetMagicRate(MagicFieldName field)
        {
            //適当計算式
            var r = DistanceWithField(field);
            if (3 - r > 1) return 3 - r;
            else return ((3 - r) + 1f) / 2;
        }

        float Distance(float px, float py, float qx, float qy)
        {
            return (float)Math.Sqrt(Math.Pow(px - qx, 2) + Math.Pow(px - qy, 2));
        }
        float DistanceWithField(MagicFieldName field)
        {
            return Distance(fieldPosX[(int)field], fieldPosY[(int)field], x, y);
        }
        float Atan2(float x, float y, MagicFieldName field)
        {
            return (float)MathEx.Atan2(fieldPosY[(int)field] - y, fieldPosX[(int)field] - x);
        }

        public override void Fluctuate(MagicFieldName field, float power)
        {
            float r1 = DistanceWithField(field);
            float r2 = DistanceWithField((MagicFieldName)((int)field + 5));
            float factor = 0.0625f; //適当な係数
            float px = x, py = y;
            float rad1 = Atan2(px, py, field) + 180;
            float rad2 = Atan2(px, py, field);
            x += (float)MathEx.Cos(rad1) * factor / (r1 * r1);
            x += (float)MathEx.Cos(rad2) * factor / (r2 * r2);
            y += (float)MathEx.Sin(rad1) * factor / (r1 * r1);
            y += (float)MathEx.Sin(rad2) * factor / (r2 * r2);

            if (Distance(x, y, 0, 0) > radius)
            {
                var r = Math.Atan2(y, x);
                x = (float)(Math.Cos(r) * radius);
                y = (float)(Math.Sin(r) * radius);
            }
        }
    }
}
