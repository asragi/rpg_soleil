using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Soleil
{
    //Put Resources here

    enum TextureID : int
    {
        FrameTest,
        WhiteWindow,
        Rule1,

        Somnia1,
        Somnia2,
        Somnia3,
        Somnia4,
        Somnia5,
        Somnia6,

        White, Size
    }

    enum AnimationID : int
    {
        Arrow,
        Size,
    }

    enum EffectAnimationID
    {
        Size,
    }

    enum ColorDictionaryID
    {
        Default = -1,
        Size,
    }

    enum FontID : int
    {
        Test,
        Size,
    }

    enum MusicID : int
    {
        Size,
    }

    enum SoundID : int
    {
        Size,
    }

    static class Resources
    {

        public static Texture2D[] Graphs;
        public static Texture2D[] Animes;
        public static Texture2D[] EffectAnimes;
        public static SpriteFont[] Fonts;
        public static List<List<string>>[] CharacterData;
        public static SoundEffect[] SEs;
        public static int[,] AnimeSplit;
        public static int[,] EffectAnimeSplit;
        public static Dictionary<Color, Color>[] ColorDictionary;
        public static List<int> OptionData;

        static string[] animePath;
        static string[] effectAnimePath;
        static string[] graphPath;
        static string[] colorDataPath;
        static string[] fontPath;
        static string[] characterDataPath;
        static string[] songPath;
        static string[] sePath;
        static string optionPath = "Data/option.csv";

        const string UIPath = "UI/";
        const string TitlePath = "Title/";
        const string MenuPath = "Menu/";
        const string StagePath = "Stage/";
        const string CharacterSelectPath = "CharacterSelect/";
        const string BattlePath = "Battle/";
        const string AkaPath = "Aka/";
        const string EffectPath = "Effect/";
        const string MusicPath = "Music/";
        const string SEPath = "SE/";
        const string DataPath = "Data/";

        public static void Init(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            //Initialize func.
            Graphs = new Texture2D[(int)TextureID.Size];
            Animes = new Texture2D[(int)AnimationID.Size];
            EffectAnimes = new Texture2D[(int)EffectAnimationID.Size];
            ColorDictionary = new Dictionary<Color, Color>[(int)ColorDictionaryID.Size];
            Fonts = new SpriteFont[(int)FontID.Size];
            SEs = new SoundEffect[(int)SoundID.Size];
            SetParamaters();

            //Load Graphics
            for (int i = 0; i < (int)TextureID.Size; i++)
                Graphs[i] = Content.Load<Texture2D>(graphPath[i]);
            for (int i = 0; i < (int)AnimationID.Size; i++)
                Animes[i] = Content.Load<Texture2D>(animePath[i]);
            for (int i = 0; i < (int)EffectAnimationID.Size; i++)
                EffectAnimes[i] = Content.Load<Texture2D>(effectAnimePath[i]);
            for (int i = 0; i < (int)FontID.Size; i++)
                Fonts[i] = Content.Load<SpriteFont>(fontPath[i]);
            for (int i = 0; i < (int)SoundID.Size; i++)
                SEs[i] = Content.Load<SoundEffect>(sePath[i]);


            
            for (int i = 0; i < (int)ColorDictionaryID.Size; i++)
                ColorDictionary[i] = ReadDictionary(colorDataPath[i]);
            
        }

        public static void ReadWindowSize()
        {
            //optionPath = "Data/option.csv";
            OptionData = CSVIO.ReadInt(optionPath).FirstOrDefault();
        }

        //画像資源の読み込みのための設定
        static void SetParamaters()
        {
            graphPath = new string[(int)TextureID.Size];
            animePath = new string[(int)AnimationID.Size];
            AnimeSplit = new int[(int)AnimationID.Size, 2];
            effectAnimePath = new string[(int)EffectAnimationID.Size];
            EffectAnimeSplit = new int[(int)EffectAnimationID.Size, 2];
            colorDataPath = new string[(int)ColorDictionaryID.Size];
            fontPath = new string[(int)FontID.Size];
            songPath = new string[(int)MusicID.Size];
            sePath = new string[(int)SoundID.Size];

            #region Load

            #region Texture

            SetPath(TextureID.White, "white");

            SetPath(TextureID.Somnia1, "Map/Back/Somnia/back2-1");
            SetPath(TextureID.Somnia2, "Map/Back/Somnia/back2-2");
            SetPath(TextureID.Somnia3, "Map/Back/Somnia/back2-3");
            SetPath(TextureID.Somnia4, "Map/Back/Somnia/back2-4");
            SetPath(TextureID.Somnia5, "Map/Back/Somnia/back2-5");
            SetPath(TextureID.Somnia6, "Map/Back/Somnia/back2-6");

            SetPath(TextureID.WhiteWindow, "UI/WindowWhite");
            SetPath(TextureID.Rule1, "UI/Rule/Rule");

            #endregion

            #region UI
            SetPath(TextureID.FrameTest, UIPath + "window2");

            #endregion


            SetPath(AnimationID.Arrow, "Arrow");
            SetSize(AnimationID.Arrow, 1, 9);

            #region EffectAnimation

            #endregion

            
            SetPath(FontID.Test, "kkmincho");
            
            //optionPath = Option.FilePath;
            #endregion 
        }

        static void SetPath(TextureID id, string path) => graphPath[(int)id] = path;
        static void SetPath(AnimationID id, string path) => animePath[(int)id] = path;
        static void SetPath(EffectAnimationID id, string path) => effectAnimePath[(int)id] = path;
        static void SetPath(ColorDictionaryID id, string path) => colorDataPath[(int)id] = path;
        static void SetPath(FontID id, string path) => fontPath[(int)id] = path;
        static void SetPath(MusicID id, string path) => songPath[(int)id] = path;
        static void SetPath(SoundID id, string path) => sePath[(int)id] = path;

        //アニメーション用の画像分割の仕方を指定
        static void SetSize(AnimationID id, int xNum, int yNum)
        {
            AnimeSplit[(int)id, 0] = xNum;
            AnimeSplit[(int)id, 1] = yNum;
        }
        static void SetSize(EffectAnimationID id, int xNum, int yNum)
        {
            EffectAnimeSplit[(int)id, 0] = xNum;
            EffectAnimeSplit[(int)id, 1] = yNum;
        }
        

        public static Texture2D GetTexture(TextureID id) => Graphs[(int)id];
        public static Texture2D GetTexture(AnimationID id) => Animes[(int)id];
        public static Texture2D GetTexture(AnimationID id, ColorDictionaryID colorID)
            => colorID == ColorDictionaryID.Default ? GetTexture(id)
            : ColorChanger.ColorChange(Animes[(int)id], ColorDictionary[(int)colorID]);
        public static Texture2D GetTexture(EffectAnimationID id) => EffectAnimes[(int)id];
        public static Texture2D GetTexture(EffectAnimationID id, ColorDictionaryID colorID) //多分使わない
            => colorID == ColorDictionaryID.Default ? GetTexture(id)
            : ColorChanger.ColorChange(EffectAnimes[(int)id], ColorDictionary[(int)colorID]);
        public static SpriteFont GetFont(FontID id) => Fonts[(int)id];
        public static SoundEffect GetSound(SoundID id) => SEs[(int)id];


        public static string GetCharacterData(int chara, int row, int col) => CharacterData[chara][row][col];
        public static int GetIntCharacterData(int chara, int row, int col) => TryParser.IntParse(GetCharacterData(chara, row, col));
        public static List<string> GetCharacterData(int chara, int row) => CharacterData[chara][row];
        public static List<int> GetIntCharacterData(int chara, int row) => GetCharacterData(chara, row).Select(TryParser.IntParse).ToList();
        public static int GetCharacterDataCount(int chara, int row) => CharacterData[chara][row].Count();
        public static int GetCharacterDataCount(int chara) => CharacterData[chara].Count();

        public static Dictionary<Color, Color> ReadDictionary(string path)
        {
            var data = CSVIO.ReadInt(path);
            var dict = new Dictionary<Color, Color>();
            int n = data[0][0];

            for (int i = 0; i < n; i++)
                dict[new Color(data[1 + i][0], data[1 + i][1], data[1 + i][2])]
                    = new Color(data[n + 1 + i][0], data[n + 1 + i][1], data[n + 1 + i][2]);

            return dict;
        }

        public static string MusicPass(MusicID id) => "Data/" + songPath[(int)id];
    }
}
