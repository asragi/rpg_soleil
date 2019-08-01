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
        IndicatorBack,
        BackBar,
        Currency,

        MessageWindow,
        ConversationWindow,

        MenuFront,
        MenuBack,
        MenuLine,
        MenuEquip1,
        MenuEquip2,
        MenuItem1,
        MenuItem2,
        MenuMagic1,
        MenuMagic2,
        MenuSkill1,
        MenuSkill2,
        MenuOption1,
        MenuOption2,
        MenuStatus1,
        MenuStatus2,
        MenuSave1,
        MenuSave2,
        MenuModalBack,
        MenuSelected,
        MenuUnselected,
        MenuStatusL,
        MenuLune,
        MenuSun,
        MenuTella,
        MenuStatusCursor,
        MenuCategory,
        IconWand,
        IconArmor,
        IconAccessary,
        IconPot,
        IconJewel,
        MagicSun,
        MagicShade,
        MagicMagic,
        MagicDark,
        MagicSound,
        MagicNinja,
        MagicWood,
        MagicMetal,
        MagicSpace,
        MagicTime,

        Rule0,
        Rule1,
        Rule2,
        Rule3,
        Rule4,
        Rule5,
        Rule6,
        Rule7,
        Rule8,
        Rule9,
        Rule10,
        Rule11,
        Rule12,
        Rule13,
        Rule14,
        Rule15,
        Rule16,
        Rule17,
        Rule18,
        Rule19,

        Flare1_1_1_1,
        Flare1_1_1_2,
        Flare1_1_2_1,
        Flare1_1_2_2,
        Flare1_1_3_1,
        Flare1_1_3_2,
        Flare1_2,
        Flare1_3,
        Flare1_4,
        Flare1_5,
        Flare1_6,
        Flare1_7,
        Flare1_8,
        Flare1_9,
        Flare1_10,
        Flare1_11,
        Flare1_12,


        Somnia1_1,
        Somnia1_2,
        Somnia1_3,
        Somnia1_4,
        Somnia1_5,

        Somnia2_1,
        Somnia2_2,
        Somnia2_3,
        Somnia2_4,
        Somnia2_5,
        Somnia2_6,

        Somnia4_1,
        Somnia4_2,
        Somnia4_3,
        Somnia4_4,
        Somnia4_5,

        Magistol1_back,
        Magistol1_dark,
        Magistol1_wall,
        Magistol1_cloth,

        Magistol2_back,
        Magistol2_chair,
        Magistol2_plant,
        Magistol2_wall,
        Magistol2_stair,
        Magistol2_shelf,

        Magistol3_back,
        Magistol3_chair,
        Magistol3_wall,

        Magistol4_back,
        Magistol4_chair1,
        Magistol4_chair2,
        Magistol4_wall,
        Magistol4_plant,

        WorldMapIcon,
        WorldMapCursor,

        White, Size
    }

    enum AnimationID : int
    {
        Arrow,

        LuneStandL,
        LuneStandR,
        LuneStandDL,
        LuneStandDR,
        LuneStandUL,
        LuneStandUR,
        LuneStandU,
        LuneStandD,

        LuneWalkL,
        LuneWalkR,
        LuneWalkDL,
        LuneWalkDR,
        LuneWalkUL,
        LuneWalkUR,
        LuneWalkU,
        LuneWalkD,

        SomniaMob1,
        SomniaAcceU,

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
        CorpM,
        CorpMini,
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
            SetPath(TextureID.BackBar, $"{UIPath}backBar");
            SetPath(TextureID.Currency, $"{UIPath}currency");

            SetPath(TextureID.Flare1_1_1_1, "Map/Back/Flare/1/flare1-1-1");
            SetPath(TextureID.Flare1_1_2_1, "Map/Back/Flare/1/flare1-1-2");
            SetPath(TextureID.Flare1_1_3_1, "Map/Back/Flare/1/flare1-1-3");
            SetPath(TextureID.Flare1_1_1_2, "Map/Back/Flare/1/flare1-1-1-2");
            SetPath(TextureID.Flare1_1_2_2, "Map/Back/Flare/1/flare1-1-2-2");
            SetPath(TextureID.Flare1_1_3_2, "Map/Back/Flare/1/flare1-1-3-2");
            for (int i = 0; i < 11; i++) SetPath(TextureID.Flare1_2 + i, "Map/Back/Flare/1/flare1-" + (i + 2));

            SetPath(TextureID.Somnia1_1, "Map/Back/Somnia/1/somnia1_1");
            SetPath(TextureID.Somnia1_2, "Map/Back/Somnia/1/somnia1_2");
            SetPath(TextureID.Somnia1_3, "Map/Back/Somnia/1/somnia1_3");
            SetPath(TextureID.Somnia1_4, "Map/Back/Somnia/1/somnia1_4");
            SetPath(TextureID.Somnia1_5, "Map/Back/Somnia/1/somnia1_5");

            SetPath(TextureID.Somnia2_1, "Map/Back/Somnia/2/somnia2-1");
            SetPath(TextureID.Somnia2_2, "Map/Back/Somnia/2/somnia2-2");
            SetPath(TextureID.Somnia2_3, "Map/Back/Somnia/2/somnia2-3");
            SetPath(TextureID.Somnia2_4, "Map/Back/Somnia/2/somnia2-4");
            SetPath(TextureID.Somnia2_5, "Map/Back/Somnia/2/somnia2-5");
            SetPath(TextureID.Somnia2_6, "Map/Back/Somnia/2/somnia2-6");

            SetPath(TextureID.Somnia4_1, "Map/Back/Somnia/4/somnia4-1");
            SetPath(TextureID.Somnia4_2, "Map/Back/Somnia/4/somnia4-2");
            SetPath(TextureID.Somnia4_3, "Map/Back/Somnia/4/somnia4-3");
            SetPath(TextureID.Somnia4_4, "Map/Back/Somnia/4/somnia4-4");
            SetPath(TextureID.Somnia4_5, "Map/Back/Somnia/4/somnia4-5");

            string mag = "Map/Back/Magistol/";

            SetPath(TextureID.Magistol1_back, mag + "room/1");
            SetPath(TextureID.Magistol1_dark, mag + "room/2");
            SetPath(TextureID.Magistol1_cloth, mag + "room/3");
            SetPath(TextureID.Magistol1_wall, mag + "room/wall");

            SetPath(TextureID.Magistol2_back, mag + "col1/col");
            SetPath(TextureID.Magistol2_chair, mag + "col1/chair");
            SetPath(TextureID.Magistol2_plant, mag + "col1/plant");
            SetPath(TextureID.Magistol2_wall, mag + "col1/wall");
            SetPath(TextureID.Magistol2_stair, mag + "col1/stair");
            SetPath(TextureID.Magistol2_shelf, mag + "col1/shelf");

            SetPath(TextureID.Magistol3_back, mag + "shop/back");
            SetPath(TextureID.Magistol3_chair, mag + "shop/chair");
            SetPath(TextureID.Magistol3_wall, mag + "shop/wall");

            SetPath(TextureID.Magistol4_back, mag + "col3/back");
            SetPath(TextureID.Magistol4_chair1, mag + "col3/chair1");
            SetPath(TextureID.Magistol4_chair2, mag + "col3/chair2");
            SetPath(TextureID.Magistol4_wall, mag + "col3/wall");
            SetPath(TextureID.Magistol4_plant, mag + "col3/plant");

            SetPath(TextureID.WhiteWindow, "UI/WindowWhite");
            SetPath(TextureID.IndicatorBack, "UI/indicatorTemp");

            SetPath(TextureID.WorldMapIcon, "Map/WorldMap/yasoba-building-icon");
            SetPath(TextureID.WorldMapCursor, "Map/WorldMap/cursor");
            #endregion

            #region Animation

            SetPath(AnimationID.LuneStandL, "Animation/Map/Character/Lune/Lune_standing_3");
            SetSize(AnimationID.LuneStandL, 1, 1);
            SetPath(AnimationID.LuneStandR, "Animation/Map/Character/Lune/Lune_standing_7");
            SetSize(AnimationID.LuneStandR, 1, 1);
            SetPath(AnimationID.LuneStandDL, "Animation/Map/Character/Lune/Lune_standing_2");
            SetSize(AnimationID.LuneStandDL, 1, 1);
            SetPath(AnimationID.LuneStandDR, "Animation/Map/Character/Lune/Lune_standing_8");
            SetSize(AnimationID.LuneStandDR, 1, 1);
            SetPath(AnimationID.LuneStandUL, "Animation/Map/Character/Lune/Lune_standing_4");
            SetSize(AnimationID.LuneStandUL, 1, 1);
            SetPath(AnimationID.LuneStandUR, "Animation/Map/Character/Lune/Lune_standing_6");
            SetSize(AnimationID.LuneStandUR, 1, 1);
            SetPath(AnimationID.LuneStandU, "Animation/Map/Character/Lune/Lune_standing_5");
            SetSize(AnimationID.LuneStandU, 1, 1);
            SetPath(AnimationID.LuneStandD, "Animation/Map/Character/Lune/Lune_standing_1");
            SetSize(AnimationID.LuneStandD, 1, 1);
            SetPath(AnimationID.LuneWalkL, "Animation/Map/Character/Lune/walking_Left");
            SetSize(AnimationID.LuneWalkL, 6, 1);
            SetPath(AnimationID.LuneWalkR, "Animation/Map/Character/Lune/walking_Right");
            SetSize(AnimationID.LuneWalkR, 6, 1);
            SetPath(AnimationID.LuneWalkDL, "Animation/Map/Character/Lune/walking_lowerleft");
            SetSize(AnimationID.LuneWalkDL, 6, 1);
            SetPath(AnimationID.LuneWalkDR, "Animation/Map/Character/Lune/walking_lowerright");
            SetSize(AnimationID.LuneWalkDR, 6, 1);
            SetPath(AnimationID.LuneWalkUL, "Animation/Map/Character/Lune/walking_upperleft");
            SetSize(AnimationID.LuneWalkUL, 6, 1);
            SetPath(AnimationID.LuneWalkUR, "Animation/Map/Character/Lune/walking_upperright");
            SetSize(AnimationID.LuneWalkUR, 6, 1);
            SetPath(AnimationID.LuneWalkU, "Animation/Map/Character/Lune/walking_up");
            SetSize(AnimationID.LuneWalkU, 6, 1);
            SetPath(AnimationID.LuneWalkD, "Animation/Map/Character/Lune/walking_down");
            SetSize(AnimationID.LuneWalkD, 6, 1);

            // Somnia
            SetPath(AnimationID.SomniaMob1, "Animation/Map/Character/Mob/somniamob1");
            SetSize(AnimationID.SomniaMob1, 3, 1);
            SetPath(AnimationID.SomniaAcceU, "Animation/Map/Character/Mob/Somnia/acce");
            SetSize(AnimationID.SomniaAcceU, 1, 1);
            #endregion

            #region UI
            SetPath(TextureID.FrameTest, UIPath + "window2");
            SetPath(TextureID.MessageWindow, UIPath + "message");

            SetPath(TextureID.ConversationWindow, UIPath + "yasoba-window");


            SetPath(TextureID.MenuFront, UIPath + MenuPath + "menufront");
            SetPath(TextureID.MenuItem1, UIPath + MenuPath + "menuitem1");
            SetPath(TextureID.MenuItem2, UIPath + MenuPath + "menuitem2");
            SetPath(TextureID.MenuMagic1, UIPath + MenuPath + "menumagic1");
            SetPath(TextureID.MenuMagic2, UIPath + MenuPath + "menumagic2");
            SetPath(TextureID.MenuSkill1, UIPath + MenuPath + "menuskill1");
            SetPath(TextureID.MenuSkill2, UIPath + MenuPath + "menuskill2");
            SetPath(TextureID.MenuOption1, UIPath + MenuPath + "menuoption1");
            SetPath(TextureID.MenuOption2, UIPath + MenuPath + "menuoption2");
            SetPath(TextureID.MenuStatus1, UIPath + MenuPath + "menustatus1");
            SetPath(TextureID.MenuStatus2, UIPath + MenuPath + "menustatus2");
            SetPath(TextureID.MenuSave1, UIPath + MenuPath + "menusave1");
            SetPath(TextureID.MenuSave2, UIPath + MenuPath + "menusave2");
            SetPath(TextureID.MenuEquip1, UIPath + MenuPath + "menuequip1");
            SetPath(TextureID.MenuEquip2, UIPath + MenuPath + "menuequip2");
            SetPath(TextureID.MenuBack, UIPath + MenuPath + "menuback");
            SetPath(TextureID.MenuLine, UIPath + MenuPath + "menuline");
            SetPath(TextureID.MenuModalBack, UIPath + MenuPath + "menuModalBack");
            SetPath(TextureID.MenuSelected, UIPath + MenuPath + "selectBack");
            SetPath(TextureID.MenuUnselected, UIPath + MenuPath + "unselectedBack");
            SetPath(TextureID.MenuLune, UIPath + MenuPath + "lune_face");
            SetPath(TextureID.MenuSun, UIPath + MenuPath + "sunny_face");
            SetPath(TextureID.MenuTella, UIPath + MenuPath + "tella_face");
            SetPath(TextureID.MenuStatusCursor, UIPath + MenuPath + "yasoba-select");
            SetPath(TextureID.MenuStatusL, UIPath + MenuPath + "MenuStatusFaceL");
            SetPath(TextureID.MenuCategory, UIPath + MenuPath + "yasoba-window-skill-category");
            SetPath(TextureID.IconWand, UIPath + MenuPath + "icons/" + "icon-wand");
            SetPath(TextureID.IconArmor, UIPath + MenuPath + "icons/" + "icon-clothes");
            SetPath(TextureID.IconAccessary, UIPath + MenuPath + "icons/" + "icon-accessary");
            SetPath(TextureID.IconPot, UIPath + MenuPath + "icons/" + "icon-pot");
            SetPath(TextureID.IconJewel, UIPath + MenuPath + "icons/" + "icon-jewel");

            string mIcon = UIPath + MenuPath + "magicicon/";
            SetPath(TextureID.MagicSun, mIcon + "micon-sun");
            SetPath(TextureID.MagicShade, mIcon + "micon-moon");
            SetPath(TextureID.MagicMagic, mIcon + "micon-magic");
            SetPath(TextureID.MagicDark, mIcon + "micon-dark");
            SetPath(TextureID.MagicSound, mIcon + "micon-sound");
            SetPath(TextureID.MagicNinja, mIcon + "micon-ninja");
            SetPath(TextureID.MagicWood, mIcon + "micon-leaf");
            SetPath(TextureID.MagicMetal, mIcon + "micon-gear");
            SetPath(TextureID.MagicSpace, mIcon + "micon-space");
            SetPath(TextureID.MagicTime, mIcon + "micon-time");

            for (int i = 0; i < 20; i++)
            {
                SetPath(TextureID.Rule0 + i, "UI/Rule/" + i);
            }
            #endregion


            SetPath(AnimationID.Arrow, "Arrow");
            SetSize(AnimationID.Arrow, 1, 9);

            #region EffectAnimation

            #endregion

            
            SetPath(FontID.CorpM, "corpm");
            SetPath(FontID.CorpMini, "corpmini");


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

        public static Vector GetSize(this FontID id, string text)
        {
            return (Vector)GetFont(id).MeasureString(text);
        }
    }
}
