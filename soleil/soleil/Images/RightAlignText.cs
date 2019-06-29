using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class RightAlignText: FontImage
    {

        Vector standardPosition;
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                PositionUpdate();
            }
        }

        public RightAlignText(FontID fontID, Vector pos, Vector? posDiff, DepthID depth, bool isStatic = true, float alpha = 0)
            : base(fontID, pos, posDiff, depth, isStatic, alpha)
        {
            standardPosition = pos;
            PositionUpdate();
        }

        private void PositionUpdate()
        {
            var diff = Pos - InitPos;
            InitPos = standardPosition - new Vector(ImageSize.X, 0);
            Pos = standardPosition + diff - new Vector(ImageSize.X, 0);
        }
    }
}
