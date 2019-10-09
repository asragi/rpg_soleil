namespace Soleil.Dungeon
{
    enum DungeonMode
    {
        Init,
        // Windows
        FirstWindow,
        // Select Go Next
        GoNext,
        // Search Dungeon
        Search,
        InitBattle,
        AfterBattle,
        FindItem,
        // ReturnHome
        ReturnConfirm,
        ReturnHome,
    }

    /// <summary>
    /// Dungeon処理に関するインスタンスを管理するクラス
    /// </summary>
    class DungeonMaster
    {
        DungeonState dungeonState;
        DungeonInput input;
        InitialWait initialWait;
        FirstSelectWindow firstSelect;
        DungeonExec dungeonSearch;
        ItemFindEvent itemFindEvent;
        InitBattle initBattle;
        AfterBattle afterBattle;
        MoveNext moveNext;
        ReturnConfirm returnConfirm;
        ReturnToHome returnToHome;
        ToastMaster toastMaster;

        // Graphics
        DungeonGraphics graphics;
        PlayerObjectWrap player;

        // Fields
        private DungeonMode mode;

        public DungeonMaster(
            DungeonName name, SceneManager sm, PersonParty party)
        {
            // States
            Mode = DungeonMode.Init;
            dungeonState = new DungeonState(name);
            player = new PlayerObjectWrap();
            // Execs
            initialWait = new InitialWait(this, player);
            dungeonSearch = new DungeonSearch(this, dungeonState);
            itemFindEvent = new ItemFindEvent(this, dungeonState);
            moveNext = new MoveNext(this, player);
            returnToHome = new ReturnToHome(player, name, sm, party);
            initBattle = new InitBattle(this);
            afterBattle = new AfterBattle(this);
            // Displays
            firstSelect = new FirstSelectWindow(this);
            returnConfirm = new ReturnConfirm(this);
            toastMaster = new ToastMaster();
            // Input
            input = new DungeonInput(this, firstSelect, returnConfirm);
            // Graphics
            graphics = new DungeonGraphics(dungeonState);
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
            dungeonState.GoNext();
            graphics.NextFloor(dungeonState);
            Reset();
        }

        public void Update()
        {
            Exec();
            input.Update();
            graphics.Update();
            player.Update();
            toastMaster.Update();
            if (KeyInput.GetKeyPush(Key.D)) toastMaster.Invoke(TextureID.IconWand, "シルバーワンド", 2);
        }

        public void Draw(Drawing d)
        {
            graphics.Draw(d);
            player.Draw(d);
            toastMaster.Draw(d);
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
                    return;
                case DungeonMode.FindItem:
                    itemFindEvent.Exec();
                    return;
                case DungeonMode.InitBattle:
                    initBattle.ExecUpdate();
                    return;
                case DungeonMode.AfterBattle:
                    afterBattle.ExecUpdate();
                    return;
                case DungeonMode.ReturnHome:
                    returnToHome.ExecUpdate();
                    return;
            }
        }

        private void OnModeChange(DungeonMode mode)
        {
            System.Console.WriteLine($"Mode Change To {mode}!");
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
            initBattle.Reset();
            afterBattle.Reset();
        }
    }
}
