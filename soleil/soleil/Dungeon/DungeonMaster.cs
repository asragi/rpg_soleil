namespace Soleil.Dungeon
{
    /// <summary>
    /// Dungeon処理に関するインスタンスを管理するクラス
    /// </summary>
    class DungeonMaster
    {
        DungeonExecutor executor;
        public DungeonMaster(DungeonName name)
        {
            executor = new DungeonExecutor(name);
        }

        public void Update()
        {
            executor.Update();
        }

        public void Draw(Drawing d)
        {

        }
    }
}
