using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu.Status
{
    class EquipDisplay : MenuComponent
    {
        const int DiffY = 34;
        FontImage[] texts;
        readonly string[] tmp = new[] {"シルバーワンド", "指定服", "ビーズのアクセサリー", "太陽の髪留め"};

        int index;
        UIImage cursor;

        StatusSystem statusSystem;
        EquipItemList equipWindow;

        public EquipDisplay(Vector pos, MenuDescription desc, StatusSystem ss)
        {
            statusSystem = ss;
            texts = new FontImage[4];
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = new FontImage(FontID.Yasashisa, pos + new Vector(0, DiffY * i), DepthID.MenuMiddle);
                texts[i].Color = ColorPalette.DarkBlue;
                texts[i].Text = tmp[i];
            }
            index = 0;
            cursor = new UIImage(TextureID.MenuSelected, texts[0].Pos, Vector.Zero, DepthID.MenuMiddle);
            equipWindow = new EquipItemList(this, desc);
            AddComponents(texts);
            SetCursorPosition();
        }

        public void OnInputDown()
        {
            if (equipWindow.Active)
            {
                equipWindow.OnInputDown();
                return;
            }
            SetIndex(1);
            SetCursorPosition();
        }

        public void OnInputUp()
        {
            if (equipWindow.Active)
            {
                equipWindow.OnInputUp();
                return;
            }
            SetIndex(-1);
            SetCursorPosition();
        }

        public void OnInputSubmit()
        {
            equipWindow.Call();
        }

        public void OnInputCancel()
        {
            if (equipWindow.Active)
            {
                equipWindow.OnInputCancel();
                return;
            }
            statusSystem.Quit();
        }

        public override void Call()
        {
            base.Call();
            cursor.Call(false);
            Reset();
        }

        public override void Quit()
        {
            base.Quit();
            cursor.Quit(false);
        }

        public override void Update()
        {
            base.Update();
            cursor.Update();
            equipWindow.Update();
        }

        public override void Draw(Drawing d)
        {
            cursor.Draw(d);
            base.Draw(d);
            equipWindow.Draw(d);
        }

        private void SetIndex(int indexDiff)
        {
            int length = texts.Length;
            index = (index + indexDiff + length) % length;
        }

        private void SetCursorPosition()
        {
            cursor.Pos = texts[index].Pos;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].Color = (i == index) ? ColorPalette.AliceBlue : ColorPalette.DarkBlue;
            }
        }
        
        private void Reset()
        {
            index = 0;
            SetCursorPosition();
        }
    }
}
