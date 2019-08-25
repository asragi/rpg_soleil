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
        DungeonExec dungeonSearch;
        MoveNext moveNext;
        ReturnConfirm returnConfirm;
        ReturnToHome returnToHome;

        // Graphics
        DungeonGraphics graphics;
        PlayerObjectWrap player;

        // Fields
        private DungeonMode mode;

        public DungeonMaster(
            DungeonName name, SceneManager sm, PersonParty party)
        {
            player = new PlayerObjectWrap();
            executor = new DungeonExecutor(name);
            initialWait = new InitialWait(this, player);
            firstSelect = new FirstSelectWindow(this);
            dungeonSearch = new DungeonSearch(this);
            moveNext = new MoveNext(this, player);
            returnConfirm = new ReturnConfirm(this);
            returnToHome = new ReturnToHome(player, name, sm, party);
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

        public void ToNextFloor()
        {
            Mode = DungeonMode.Init;
            Reset();
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
                    initialWait.ExecUpdate();
                    return;
                case DungeonMode.GoNext:
                    moveNext.ExecUpdate();
                    break;
                case DungeonMode.Search:
                    dungeonSearch.ExecUpdate();
                    break;
                case DungeonMode.ReturnHome:
                    returnToHome.ExecUpdate();
                    return;
            }
        }

        private void OnModeChange(DungeonMode mode)
        {
            switch (mode)
            {
                case DungeonMode.Init:
                    Transition.GetInstance().SetMode(TransitionMode.FadeIn);
                    return;
                case DungeonMode.FirstWindow:
                    firstSelect.Call();
                    return;
                case DungeonMode.ReturnConfirm:
                    returnConfirm.Call();
                    return;
            }
        }

        private void Reset()
        {
            initialWait.Reset();
            player.Reset();
            moveNext.Reset();
            dungeonSearch.Reset();
        }
    }
}
