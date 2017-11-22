using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GCP
{
    //同時進行しない演出
    abstract class CutIn
    {
        protected List<Effect> effects;
        public int ownPlayerNumber;
        protected int Frame;
        protected int limitFrame;
        public CutIn(int limitFrame,int ownPlayerNumber, List<Effect> effects)
        {
            this.limitFrame = limitFrame;
            this.ownPlayerNumber = ownPlayerNumber;
            this.effects = effects;
        }

        public virtual bool Move()
        {
            Frame++;
            if (limitFrame <= Frame)
                return true;
            return false;
        }
    }

    class Blackout : CutIn
    {
        const float Alpha = 0.3f;
        public Blackout(int limitFrame, int ownPlayerNumber, List<Effect> effects)
            :base(limitFrame, ownPlayerNumber, effects)
        {

        }

        public override bool Move()
        {
            if (Frame == 0)
                effects.Add(new BoxAbsoluteEffect(new Vector(Game1.VirtualCenterX, Game1.VirtualCenterY), 
                    new Vector(Game1.VirtualWindowSizeX, Game1.VirtualWindowSizeY), limitFrame, effects, Color.Black*Alpha));

            return base.Move();
        }
    }
}
