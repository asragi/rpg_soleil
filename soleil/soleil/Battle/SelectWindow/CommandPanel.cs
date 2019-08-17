using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soleil.Menu;

namespace Soleil.Battle
{
    enum CommandEnum
    {
        Magic,
        Skill,
        Guard,
        Escape,
    }
    class CommandPanel : SelectablePanel
    {
        public override string Desctiption => "a";
        readonly CommandEnum command;
        Image selected, unselected;
        public CommandPanel(CommandEnum command, BasicMenu parent) : base(parent)
        {
            this.command = command;
            TextureID selectedID, unselectedID;
            switch (command)
            {
                case CommandEnum.Magic:
                    (selectedID, unselectedID) = (TextureID.BattleCommandSelectedMagic, TextureID.BattleCommandUnselectedMagic);
                    break;
                case CommandEnum.Skill:
                    (selectedID, unselectedID) = (TextureID.BattleCommandSelectedSkill, TextureID.BattleCommandUnselectedSkill);
                    break;
                case CommandEnum.Guard:
                    (selectedID, unselectedID) = (TextureID.BattleCommandSelectedGuard, TextureID.BattleCommandUnselectedGuard);
                    break;
                case CommandEnum.Escape:
                    (selectedID, unselectedID) = (TextureID.BattleCommandSelectedEscape, TextureID.BattleCommandUnselectedEscape);
                    break;
                default:
                    (selectedID, unselectedID) = (0, 0);
                    break;
            }
            selected = new Image(selectedID, LocalPos + parent.Pos, ItemNumPosDiff, DepthID.Status);
            unselected = new Image(unselectedID, LocalPos + parent.Pos, ItemNumPosDiff, DepthID.Status);
        }

        public override void Fade(int duration, Func<double, double, double, double, double> _easeFunc, bool isFadeIn)
        {
            if (isFadeIn)
            {
                if (IsSelected)
                    selected.Fade(duration, _easeFunc, isFadeIn);
                else
                    unselected.Fade(duration, _easeFunc, isFadeIn);
            }
            else
                selected.Fade(duration, _easeFunc, isFadeIn);
            unselected.Fade(duration, _easeFunc, isFadeIn);

        }

        public override void MoveTo(Vector target, int duration, Func<double, double, double, double, double> _easeFunc)
        {
            selected.MoveTo(target, duration, _easeFunc);
            unselected.MoveTo(target, duration, _easeFunc);
        }

        protected override void OnSelected()
        {
            selected.Fade(20, MenuSystem.EaseFunc, true);
            unselected.Fade(20, MenuSystem.EaseFunc, false);
        }

        /// <summary>
        /// 選択が解除された瞬間に行われる処理．
        /// </summary>
        protected override void OnUnselected()
        {
            selected.Fade(5, MenuSystem.EaseFunc, false);
            unselected.Fade(5, MenuSystem.EaseFunc, true);
        }

        public override void Update()
        {
            selected.Update();
            selected.Pos = LocalPos + BasicMenu.Pos;
            unselected.Update();
            unselected.Pos = LocalPos + BasicMenu.Pos;
        }

        public override void Draw(Drawing d)
        {
            selected.Draw(d);
            unselected.Draw(d);
        }
    }
}
