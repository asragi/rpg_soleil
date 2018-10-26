using Soleil.Event.Shop;
using Soleil.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Detail
{
    /// <summary>
    /// 所持数の表示
    /// </summary>
    class PossessNum
    {
        readonly String ExplainText = "所持数";
        readonly Vector NumDiff = new Vector(200, 0);
        ItemList items = PlayerBaggage.GetInstance().Items;
        FontImage explainText;
        FontImage possessText;
        public PossessNum(Vector _pos)
        {
            possessText = new FontImage(FontID.Test, _pos + NumDiff, DepthID.Message, true, 0);
            explainText = new FontImage(FontID.Test, _pos, DepthID.Message, true, 0);
        }

        void Refresh(SelectablePanel panel)
        {
            if(panel is MagicMenuPanel)
            {
                explainText.Text = "";
                possessText.Text = "";
                return;
            }
            if(panel is ItemPanel i)
            {
                explainText.Text = ExplainText;
                possessText.Text = items.GetItemNum(i.ID).ToString();
            }
            if (panel is ShopPanel s)
            {
                explainText.Text = ExplainText;
                possessText.Text = items.GetItemNum(s.ID).ToString();
            }
        }

        public void Call()
        {
            possessText.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
            explainText.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, true);
        }

        public void Quit()
        {
            possessText.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
            explainText.Fade(MenuSystem.FadeSpeed, MenuSystem.EaseFunc, false);
        }

        public void Update(SelectablePanel panel)
        {
            Refresh(panel);
            possessText.Update();
            explainText.Update();
        }

        public void Draw(Drawing d)
        {
            possessText.Draw(d);
            explainText.Draw(d);
        }
    }
}
