using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    enum WorldPointKey
    {
        Flare = 1,
        Somnia = 2,
        Magistol = 3,
        Parel = 5,
        Shimaki = 6,
        Earthband = 7,
        AisenBerz = 8,
        size,
    }

    /// <summary>
    /// 地図上に存在する町などの各施設を表すクラス．
    /// </summary>
    class WorldPoint
    {
        public static readonly Dictionary<WorldPointKey, string> Descriptions = new Dictionary<WorldPointKey, string>()
        {
            {WorldPointKey.Flare, "太陽と潮騒の街\n陽術を習得可能" },
            {WorldPointKey.Somnia, "影と秘密ある街\n陰術を習得可能" },
            {WorldPointKey.Magistol, "魔法学校の都市\n魔術を習得可能" },
            {WorldPointKey.Parel, "芸術と音楽の街\n音術を習得可能" },
            {WorldPointKey.Shimaki, "忍ぶ者の隠れ処\n忍術を習得可能" },
            {WorldPointKey.Earthband, "大自然が包む街\n樹術を習得可能" },
            {WorldPointKey.AisenBerz, "金属と孤独の街\n鋼術を習得可能" },
        };
        public readonly WorldPointKey ID; // csvの頂点のIDと一致させる．
        public readonly Vector Pos; // ワールドマップ上の座標．
        public Dictionary<WorldPointKey, int> Edges;

        public bool IsPlayerIn;
        public bool Selected;

        UIImage icon;

        public WorldPoint(WorldPointKey id, Vector position)
        {
            ID = id;
            Pos = position;
            Edges = new Dictionary<WorldPointKey, int>();
            icon = new UIImage(TextureID.WorldMapIcon, Pos, Vector.Zero, DepthID.HitBox, true, false, 1);
        }

        public void SetEdge(WorldPointKey key, int cost) => Edges.Add(key, cost);

        public void Draw(Drawing d)
        {
            icon.Color = IsPlayerIn ? Color.Crimson : Color.AliceBlue;
            icon.Draw(d);
        }
    }
}
