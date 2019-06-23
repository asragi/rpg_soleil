using Soleil.Item;
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

        Person displayingCharacter;

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
            if (equipWindow.Active)
            {
                equipWindow.OnInputSubmit();
                return;
            }
            ItemType targetItemType;
            if (index == 0) targetItemType = ItemType.Weapon;
            else if (index == 1) targetItemType = ItemType.Armor;
            else targetItemType = ItemType.Accessory;
            var targetEquip = displayingCharacter.Equip;
            equipWindow.CallWithData(targetEquip, targetItemType);
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

        public void Call(Person target)
        {
            displayingCharacter = target;
            RefreshEquipText();
            cursor.Call(false);
            base.Call();
            Reset();
        }

        public void RefreshEquipText()
        {
            var targetEquip = displayingCharacter.Equip;
            texts[0].Text = targetEquip.Weapon.Name;
            texts[1].Text = targetEquip.Armor.Name;
            texts[2].Text = targetEquip.Accessary[0].Name;
            texts[3].Text = targetEquip.Accessary[1].Name;
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
