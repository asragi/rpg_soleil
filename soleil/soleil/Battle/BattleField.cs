using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    enum Side
    {
        Left,
        Right,
        Size,
    }
    class BattleField
    {
        List<CharacterStatus> charas;
        List<int>[] campIndex;
        List<Side> sides;
        MagicField magicField;
        public BattleField()
        {
            campIndex = new List<int>[(int)Side.Size];
            for (int i = 0; i < 2; i++)
                campIndex[i] = new List<int>();

            charas.Add(new CharacterStatus());
            sides.Add(Side.Right);
            charas.Add(new CharacterStatus());
            sides.Add(Side.Right);
            charas.Add(new CharacterStatus());
            sides.Add(Side.Left);
            charas.Add(new CharacterStatus());
            sides.Add(Side.Left);
            charas.Add(new CharacterStatus());
            sides.Add(Side.Left);

            magicField = new SimpleMagicField();
        }

        bool select = false;
        enum Command
        {
            Attack,
            Magic,
            Item,
            Escape,
            Size,
        }
        Command selectCommand;
        public void Move()
        {

            if(select)
            {
                if (KeyInput.GetKeyPush(Key.Down))
                {
                    selectCommand++;
                    if (selectCommand == Command.Size)
                    {
                        selectCommand = Command.Attack;
                    }
                }
                if (KeyInput.GetKeyPush(Key.Up))
                {
                    if (selectCommand == Command.Attack)
                    {
                        selectCommand = Command.Size;
                    }
                    selectCommand--;
                }
                if (KeyInput.GetKeyPush(Key.A))
                {

                }
            }
        }

        public void Draw()
        {

        }
    }
}
