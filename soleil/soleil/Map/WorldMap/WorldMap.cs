﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil.Map.WorldMap
{
    class WorldMap
    {
        private static readonly Texture2D WorldMapTexture = Resources.GetTexture(TextureID.WorldMap);
        public readonly static Vector WorldMapSize = new Vector(WorldMapTexture.Width, WorldMapTexture.Height);
        WorldPoint[] points;
        WorldMapPlayerIcon playerIcon;
        Image worldmap;

        public WorldMap(WorldPointKey initialPosition, BoxManager _boxManager)
        {
            points = MakePoints(_boxManager);
            SetPlayerPosition(initialPosition);
            playerIcon = new WorldMapPlayerIcon(GetPlayerPoint());
            worldmap = new Image(TextureID.WorldMap, Vector.Zero, DepthID.BackGround, isStatic: false, alpha: 1);

            WorldPoint[] MakePoints(BoxManager _bm)
            {
                var result = new WorldPoint[(int)WorldPointKey.size];
                Set(WorldPointKey.Flare, new Vector(1703, 1036), result, _bm);
                Set(WorldPointKey.Somnia, new Vector(1786, 722), result, _bm);
                Set(WorldPointKey.Magistol, new Vector(1428, 753), result, _bm);
                Set(WorldPointKey.Parel, new Vector(1182, 923), result, _bm);
                Set(WorldPointKey.Shimaki, new Vector(1865, 535), result, _bm);
                Set(WorldPointKey.Earthband, new Vector(873, 716), result, _bm);
                Set(WorldPointKey.AisenBerz, new Vector(1923, 1237), result, _bm);

                SetEdge(WorldPointKey.Flare, WorldPointKey.Somnia, 4, result);
                SetEdge(WorldPointKey.Flare, WorldPointKey.Magistol, 3, result);
                /*
                SetEdge(WorldPointKey.Flare, WorldPointKey.Parel, 3, result);
                SetEdge(WorldPointKey.Flare, WorldPointKey.AisenBerz, 9, result);
                SetEdge(WorldPointKey.Somnia, WorldPointKey.Shimaki, 8, result);
                SetEdge(WorldPointKey.Magistol, WorldPointKey.Parel, 3, result);
                SetEdge(WorldPointKey.Parel, WorldPointKey.Earthband, 7, result);
                */
                return result;

                void Set(WorldPointKey key, Vector pos, WorldPoint[] array, BoxManager bm)
                    => array[(int)key] = new WorldPoint(key, pos, bm);

                void SetEdge(WorldPointKey a, WorldPointKey b, int cost, WorldPoint[] array)
                {
                    array[(int)a].SetEdge(b, cost);
                    array[(int)b].SetEdge(a, cost);
                }
            }
        }

        public void SetPlayerPosition(WorldPointKey position)
        {
            foreach (var item in points)
            {
                if (item == null) continue;
                item.IsPlayerIn = item.ID == position;
            }
        }

        public WorldPoint GetPoint(WorldPointKey key) => points[(int)key];

        public WorldPoint GetPlayerPoint()
        {
            foreach (var item in points)
            {
                if (item == null) continue;
                if (item.IsPlayerIn) return item;
            }
            return null;
        }

        public void SetPlayerPos(Vector pos) => playerIcon.Pos = pos;

        public void Update()
        {
            worldmap.Update();
            playerIcon.Update();
        }

        public void Draw(Drawing d)
        {
            playerIcon.Draw(d);
            worldmap.Draw(d);
            points.ForEach2(p => p?.Draw(d));
        }
    }
}
