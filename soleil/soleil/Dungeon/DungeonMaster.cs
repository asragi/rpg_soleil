namespace Soleil.Dungeon
{
    enum DungeonMode
    {
        Init,
        FirstWindow,
        GoNext,
        Search,
        ReturnConfirm,
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
        ReturnConfirm returnConfirm;

        // Graphics
        DungeonGraphics graphics;
        PlayerObjectWrap player;

        // Fields
        private DungeonMode mode;

        public DungeonMaster(DungeonName name)
        {
            player = new PlayerObjectWrap();
            executor = new DungeonExecutor(name);
            initialWait = new InitialWait(this, player);
            firstSelect = new FirstSelectWindow(this);
            returnConfirm = new ReturnConfirm(this);
            input = new DungeonInput(this, firstSelect, returnConfirm);
            graphics = new DungeonGraphics();
            Mode = DungeonMode.Init;
        }

        public DungeonMode Mode {
            get => mode;
            set {
                if (mode != value)
                {
                    OnModeChange(value);
                }
                mode = value;
            }
        }

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

        private void OnModeChange(DungeonMode mode)
        {
            switch (mode)
            {
                case DungeonMode.FirstWindow:
                    firstSelect.Call();
                    return;
                case DungeonMode.ReturnConfirm:
                    returnConfirm.Call();
                    return;
            }
        }
    }
}
