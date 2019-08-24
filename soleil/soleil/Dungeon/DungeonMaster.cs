﻿namespace Soleil.Dungeon
{
    enum DungeonMode
    {
        Init,
        FirstWindow,
        GoNext,
        Search,
        ReturnHome,
    }

    /// <summary>
    /// Dungeon処理に関するインスタンスを管理するクラス
    /// </summary>
    class DungeonMaster
    {
        DungeonExecutor executor;
        DungeonInput input;
        InitialWait initialWait;
        FirstSelectWindow firstSelect;

        // Graphics
        DungeonGraphics graphics;
        PlayerObjectWrap player;

        public DungeonMaster(DungeonName name)
        {
            player = new PlayerObjectWrap();
            executor = new DungeonExecutor(name);
            initialWait = new InitialWait(this);
            firstSelect = new FirstSelectWindow(this);
            firstSelect.Call();
            input = new DungeonInput(this, firstSelect);
            graphics = new DungeonGraphics();
            Mode = DungeonMode.Init;
        }

        public DungeonMode Mode { get; set; }

        public void Update()
        {
            Exec();
            executor.Update();
            input.Update();
            graphics.Update();
            player.Update();
        }

        public void Draw(Drawing d)
        {
            graphics.Draw(d);
            player.Draw(d);
        }

        private void Exec()
        {
            switch (Mode)
            {
                case DungeonMode.Init:
                    initialWait.Exec();
                    return;
                case DungeonMode.FirstWindow:
                    break;
                case DungeonMode.GoNext:
                    break;
                case DungeonMode.Search:
                    break;
                case DungeonMode.ReturnHome:
                    break;
                default:
                    break;
            }
        }
    }
}
