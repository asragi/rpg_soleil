using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Battle
{
    //MenuComponent継承してもいいかも
    class StatusUI
    {
        int maxHP, maxMP;
        int HP, MP;
        int drawingHP, drawingMP;
        TextImage HPImage, MPImage;
        TextImage maxHPImage, maxMPImage;

        //画像が用意されそう
        TextImage HPText, MPText;

        /// <summary>
        /// HPとMPの表示Y座標の中央からの差
        /// </summary>
        const int LineDiff = 20;

        /// <summary>
        /// "HP : "の表示位置
        /// </summary>
        const int LetterPosX = -50;

        /// <summary>
        /// HP表示位置
        /// </summary>
        const int NumberPosX = 30;

        /// <summary>
        /// maxHPの表示位置のずらし量
        /// この分だけ右下にずれる
        /// </summary>
        const int MaxDiff = 8;

        public StatusUI(int maxHP, int maxMP, Vector pos)
        {
            (this.maxHP, this.maxMP) = (maxHP, maxMP);
            (HP, MP) = (maxHP, maxMP);
            (drawingHP, drawingMP) = (maxHP, maxMP);
            HPImage = new TextImage(FontID.CorpM, pos + new Vector(NumberPosX, -LineDiff), DepthID.Status, alpha: 1);
            MPImage = new TextImage(FontID.CorpM, pos + new Vector(NumberPosX, LineDiff), DepthID.Status, alpha: 1);
            maxHPImage = new TextImage(FontID.CorpM, pos + new Vector(NumberPosX + MaxDiff, -LineDiff + MaxDiff), DepthID.Status, alpha: 0.5f);
            maxMPImage = new TextImage(FontID.CorpM, pos + new Vector(NumberPosX + MaxDiff, LineDiff + MaxDiff), DepthID.Status, alpha: 0.5f);
            maxHPImage.Text = maxHP.ToString();
            maxMPImage.Text = maxMP.ToString();
            HPText = new TextImage(FontID.CorpM, pos + new Vector(LetterPosX, -LineDiff), DepthID.Status, alpha: 1);
            MPText = new TextImage(FontID.CorpM, pos + new Vector(LetterPosX, LineDiff), DepthID.Status, alpha: 1);
            HPText.Text = "HP : ";
            MPText.Text = "MP : ";
        }

        public void Damage(int decreasedHP = 0, int decreasedMP = 0)
        {
            HP = MathEx.Clamp(HP - decreasedHP, maxHP, 0);
            MP = MathEx.Clamp(MP - decreasedMP, maxMP, 0);
        }

        const int Amount = 1;
        public void Update()
        {
            var substHP = MathEx.AbsoluteMinus(drawingHP - HP, Amount);
            drawingHP = substHP + HP;
            var substMP = MathEx.AbsoluteMinus(drawingMP - MP, Amount);
            drawingMP = substMP + MP;

            HPImage.Text = drawingHP.ToString();
            MPImage.Text = drawingMP.ToString();
        }

        public void Draw(Drawing sb)
        {
            HPText.Draw(sb);
            MPText.Draw(sb);
            maxHPImage.Draw(sb);
            maxMPImage.Draw(sb);
            HPImage.Draw(sb);
            MPImage.Draw(sb);
        }
    }
}
