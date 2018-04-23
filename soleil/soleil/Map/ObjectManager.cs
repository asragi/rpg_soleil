﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class ObjectManager
    {
        List<MapObject> objects;
        PlayerObject player;

        public ObjectManager()
        {
            objects = new List<MapObject>();
        }

        public void Add(MapObject obj)
        {
            objects.Add(obj);
        }

        public void SetPlayer(PlayerObject pl) => player = pl;
        public PlayerObject GetPlayer() => player;

        public void Update()
        {
            objects.RemoveAll(s => s.IsDead());
            foreach (var item in objects)
            {
                item.Update();
            }
        }

        public void Draw(Drawing sb)
        {
            foreach (var item in objects)
            {
                item.Draw(sb);
            }
        }
    }
}
