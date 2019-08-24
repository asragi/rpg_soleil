namespace Soleil.Dungeon
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
        FirstSelectWindow firstSelect;
        DungeonGraphics graphics;

        public DungeonMaster(DungeonName name)
        {
            executor = new DungeonExecutor(name);
            firstSelect = new FirstSelectWindow();
            firstSelect.Call();
            input = new DungeonInput(this, firstSelect);
            graphics = new DungeonGraphics();
            Mode = DungeonMode.Init;
        }

        public DungeonMode Mode { get; set; }

        public void Update()
        {
            executor.Update();
            input.Update();
            graphics.Update();
        }

        public void Draw(Drawing d)
        {
            graphics.Draw(d);
        }
    }
}
