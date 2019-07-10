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
        readonly Vector Space = TextSelectablePanel.Spacing;
        readonly Vector IconSpace = ItemPanelBase.IconSpace;
        const int DiffY = 34;
        TextImage[] texts;
        Image[] icons;
        readonly TextureID[] ids = new[] { TextureID.IconWand, TextureID.IconArmor, TextureID.IconAccessary, TextureID.IconAccessary };
        IItem[] equips;

        Person displayingCharacter;

        int index;
        Image cursor;
        MenuDescription description;

        StatusSystem statusSystem;
        EquipItemList equipWindow;

        public EquipDisplay(Vector pos, MenuDescription desc, StatusSystem ss)
        {
            statusSystem = ss;
            texts = new TextImage[4];
            icons = new Image[4];
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = new TextImage(FontID.CorpM, pos + new Vector(0, DiffY * i) + Space, DepthID.MenuMiddle);
                texts[i].Color = ColorPalette.DarkBlue;
                icons[i] = new Image(ids[i], pos + new Vector(0, DiffY * i) + IconSpace, Vector.Zero, DepthID.MenuMiddle);
                icons[i].Color = ColorPalette.DarkBlue;
            }
            index = 0;
            cursor = new Image(TextureID.MenuSelected, texts[0].Pos, Vector.Zero, DepthID.MenuMiddle);
            equipWindow = new EquipItemList(this, desc);
            description = desc;
            AddComponents(texts);
            AddComponents(icons);
            SetCursorPosition();
        }

        public void OnInputDown()
        {
            if (equipWindow.Active)
            {
                equipWindow.OnInputDown();
                return;
            }
            ChangeIndex(1);
            SetCursorPosition();
        }

        public void OnInputUp()
        {
            if (equipWindow.Active)
            {
                equipWindow.OnInputUp();
                return;
            }
            ChangeIndex(-1);
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
            Refresh();
            cursor.Call(false);
            base.Call();
            Reset();
        }

        public void Refresh()
        {
            // 装備リストの更新
            equips = displayingCharacter.Equip.GetEquipDataSet();
            for (int i = 0; i < equips.Length; i++)
            {
                texts[i].Text = equips[i].Name;
            }
            // Status表示の他要素の更新
            statusSystem.Refresh(displayingCharacter);
            RefreshDescription();
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

        private void ChangeIndex(int indexDiff)
        {
            int length = texts.Length;
            index = (index + indexDiff + length) % length;
            RefreshDescription();
        }

        private void SetCursorPosition()
        {
            cursor.Pos = texts[index].Pos - Space;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].Color = (i == index) ? ColorPalette.AliceBlue : ColorPalette.DarkBlue;
                icons[i].Color = (i == index) ? ColorPalette.AliceBlue : ColorPalette.DarkBlue;
            }
        }
        
        private void Reset()
        {
            index = 0;
            SetCursorPosition();
            Refresh();
        }

        private void RefreshDescription()
        {
            description.Text = equips[index].Description;
        }
    }
}
