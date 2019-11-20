using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Soleil
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Drawing drawing;
        SceneManager sm;
        PersonParty party;

        //実際の画面サイズ
        public const int VirtualWindowSizeX = 960;
        public const int VirtualWindowSizeY = 540;

        //拡大して描画
        public static bool IsFullScreen = false;
        public static int DrawRate = 1;
        public static int WindowSizeX => (int)(VirtualWindowSizeX * DrawRate);
        public static int WindowSizeY => (int)(VirtualWindowSizeY * DrawRate);
        public static int GameCenterX => WindowSizeX / 2;
        public static int GameCenterY => WindowSizeY / 2;
        public static int VirtualCenterX => VirtualWindowSizeX / 2;
        public static int VirtualCenterY => VirtualWindowSizeY / 2;

        public Game1()
        {
            //タイトル
            this.Window.Title = "soleil";

            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            //画面サイズを決める
            graphics.SynchronizeWithVerticalRetrace = true;   //垂直同期

            ReadWindowSize();

            graphics.PreferredBackBufferWidth = WindowSizeX;
            graphics.PreferredBackBufferHeight = WindowSizeY;
            if (IsFullScreen)
                graphics.ToggleFullScreen();
            graphics.ApplyChanges();

        }

        void ReadWindowSize()
        {
            Resources.ReadWindowSize();

            if (Resources.OptionData?.Count >= 2)
            {
                if (Resources.OptionData[0] != 0)
                    DrawRate = Resources.OptionData[0];
                if (Resources.OptionData[1] != 0)
                    IsFullScreen = true;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            drawing = new Drawing(spriteBatch, new Drawing3D(GraphicsDevice));

            LoadItemData(); // Debug
            // SceneManager
            sm = SceneManager.GetInstance();
            // !debug! Save
            bool saveExist = SaveLoad.FileExist();
            if (saveExist) SaveLoad.Load();
            party = SaveLoad.GetParty(isNew: !saveExist);

            new TitleScene(sm);
            //new TestBattleScene(sm, party, new[] { Battle.CharacterType.TestEnemy, Battle.CharacterType.TestEnemy });
            // new WorldMapScene(sm, party, Map.WorldMap.WorldPointKey.Somnia);
            //new DungeonScene(sm, party, Dungeon.DungeonName.MagistolUnderground);


            drawing.DrawRate = DrawRate;

            void LoadItemData() // Debug 本来はセーブデータのロードやニューゲーム時に行いたい．
            {
                var playerBaggage = PlayerBaggage.GetInstance();
                var item = new Item.ItemList();

                item.AddItem(Item.ItemID.Portion);
                item.AddItem(Item.ItemID.Zarigani);

                var wallet = new Map.MoneyWallet(50000);
                playerBaggage.SetData(item, wallet);
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ColorChanger.Init(GraphicsDevice);
            Resources.Init(Content);
            KeyInput.Init();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        public static int frame = 0;
        public static bool End = false;
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            if (End)
            {
                Exit();
                End = false;
            }


            // TODO: Add your update logic here
            //Audio.Update();
            KeyInput.Update();
            sm.Update();
            frame++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            drawing.DrawBegin();
            sm.Draw(drawing);
            drawing.DrawEnd();
            base.Draw(gameTime);
        }
    }
}
