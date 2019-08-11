using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Soleil
{

    abstract class Effect
    {
        public Vector Position;
        protected int frame = 0;
        protected List<Effect> effect;
        public bool Disable = false;    //trueで破棄
        protected Color Color;
        public Effect(Vector position, List<Effect> effect)
        {
            Position = position;
            this.effect = effect;
        }
        public Effect(Vector position, List<Effect> effect, Color color)
        {
            Position = position;
            this.effect = effect;
            Color = color;
        }

        public virtual void Move()
        {
            frame++;
        }



        public virtual void Draw(Drawing d) { }
    }

    class BoxAbsoluteEffect : Effect
    {
        int frameLimit;
        Vector size;
        public BoxAbsoluteEffect(Vector position, Vector size, int frame, List<Effect> effect)
            : base(position, effect)
        {
            frameLimit = frame;
            this.size = size;
        }
        public BoxAbsoluteEffect(Vector position, Vector size, int frame, List<Effect> effect, Color color)
            : base(position, effect, color)
        {
            frameLimit = frame;
            this.size = size;
        }

        public override void Move()
        {
            base.Move();
            if (frame >= frameLimit)
                Disable = true;
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            d.DrawBoxStatic(Position, size, Color, DepthID.Effect);
        }
    }

    class TextureEffect : Effect
    {
        Texture2D texture;
        int frameLimit;
        bool flip;
        public TextureEffect(Vector position, Texture2D texture, int frame, bool flip, List<Effect> effect)
            : base(position, effect)
        {
            this.texture = texture;
            frameLimit = frame;
            this.flip = flip;
        }
        public TextureEffect(Vector position, Texture2D texture, int frame, bool flip, List<Effect> effect, Color color)
            : base(position, effect, color)
        {
            this.texture = texture;
            frameLimit = frame;
            this.flip = flip;
        }

        public override void Move()
        {
            base.Move();
            if (frame >= frameLimit)
                Disable = true;
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            if (flip)
                d.DrawFlipHorizontally(Position, texture, DepthID.Effect);
            else
                d.Draw(Position, texture, DepthID.Effect);
        }
    }

    class TextureAbsoluteEffect : Effect
    {
        Texture2D texture;
        int frameLimit;
        bool flip;
        public TextureAbsoluteEffect(Vector position, Texture2D texture, int frame, bool flip, List<Effect> effect)
            : base(position, effect)
        {
            this.texture = texture;
            frameLimit = frame;
            this.flip = flip;
        }
        public TextureAbsoluteEffect(Vector position, Texture2D texture, int frame, bool flip, List<Effect> effect, Color color)
            : base(position, effect, color)
        {
            this.texture = texture;
            frameLimit = frame;
            this.flip = flip;
        }

        public override void Move()
        {
            base.Move();
            if (frame >= frameLimit)
                Disable = true;
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            if (flip)
                d.DrawStaticFlipHorizontally(Position, texture, DepthID.Effect);
            else
                d.DrawStatic(Position, texture, DepthID.Effect);
        }
    }

    class AnimationEffect : Effect
    {
        Animation animation;
        bool flip;
        public AnimationEffect(Vector position, EffectAnimationData animationData, bool flip, List<Effect> effect)
            : base(position, effect)
        {
            animation = new Animation(animationData);
            this.flip = flip;
        }
        public AnimationEffect(Vector position, EffectAnimationData animationData, bool flip, List<Effect> effect, Color color)
            : base(position, effect, color)
        {
            animation = new Animation(animationData);
            this.flip = flip;
        }

        public override void Move()
        {
            base.Move();
            animation.Move();
            if (animation.IsEnd())
                Disable = true;
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
            if (flip)
                animation.DrawFlipHorizontally(d, Position);
            else
                animation.Draw(d, Position);
        }
    }


    class ShakeWindowEffect : Effect
    {
        int limitFrame;
        bool onFlag, offFlag;
        Camera camera;
        public ShakeWindowEffect(int frame, Camera camera, List<Effect> effect)
            : base(new Vector(-3, 3), effect)
        {
            this.camera = camera;
            limitFrame = frame;
        }
        public ShakeWindowEffect(Vector position, int frame, List<Effect> effect)
            : base(position, effect)
        {
            limitFrame = frame;
        }

        public override void Move()
        {
            base.Move();
            if (!onFlag)
            {
                camera.delta += Position;
                onFlag = true;
            }

            if (frame >= limitFrame - 1 && !offFlag)
            {
                camera.delta -= Position;
                offFlag = true;
                Disable = true;
            }
        }

        public override void Draw(Drawing d)
        {
            base.Draw(d);
        }
    }
}