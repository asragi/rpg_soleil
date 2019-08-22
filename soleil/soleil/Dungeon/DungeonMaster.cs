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
        public DungeonMaster(DungeonName name)
        {
            executor = new DungeonExecutor(name);
            firstSelect = new FirstSelectWindow();
            firstSelect.Call();
            input = new DungeonInput(this, firstSelect);
            Mode = DungeonMode.Init;
        }

        public DungeonMode Mode { get; set; }

        public void Update()
        {
            executor.Update();
            input.Update();
        }

        public void Draw(Drawing d)
        {

        }
    }
}
