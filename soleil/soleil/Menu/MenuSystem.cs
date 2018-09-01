using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Menu
{
    enum MenuName
    {
        Items = 0,
        Magic,
        Equip,
        Status,
        Option,
        Save,
        size,
    }
    class MenuSystem
    {
        bool isActive;
        Image backImage, frontImage;
        MenuItem[] menuItems;
        int index;

        public MenuSystem()
        {
            index = 0;
            isActive = false;
            // Image初期化
            backImage = new Image(0, Resources.GetTexture(TextureID.WhiteWindow), Vector.Zero, DepthID.MessageBack, false, true);
            frontImage = new Image(0, Resources.GetTexture(TextureID.MenuFront), Vector.Zero, DepthID.MessageBack, false, true);
            for (int i = 0; i < (int)MenuName.size; i++)
            {
                // i==0 のみ selected=trueとする
                menuItems[i] = new MenuItem((MenuName)i, i == 0);
            }
        }

        /// <summary>
        /// メニューを呼び出す
        /// </summary>
        public void CallMenu()
        {
            isActive = true;
        }

        /// <summary>
        /// メニューを閉じる
        /// </summary>
        public void QuitMenu()
        {
            isActive = false;
        }

        public void Update()
        {
            // ImageUpdate
            backImage.Update();
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Update();
            }
            frontImage.Update();
        }

        public void Draw(Drawing d)
        {
            // Activeでなければ実行しない
            if (!isActive) return;
            // 背景描画
            backImage.Draw(d);
            // 選択肢描画
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Draw(d);
            }
            // 前景描画
            frontImage.Draw(d);
        }
    }
}
