﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Soleil
{
    class CSVIO
    {
        public static List<List<int>> ReadInt(string fileName)
        {
            List<List<int>> val = new List<List<int>>();
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string tmp = sr.ReadLine();

                        if (tmp == "") continue;
                        //行頭に//でスキップ
                        if (tmp.Substring(0, 2) == "//") continue;


                        val.Add(tmp.Split(',').Select(s => { int t; int.TryParse(s, out t); return t; }).ToList());
                    }
                }
            }
            catch { }
            return val;
        }
        public static List<List<string>> Read(string fileName)
        {
            List<List<string>> val = new List<List<string>>();
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string tmp = sr.ReadLine();

                        if (tmp == "") continue;
                        //行頭に//でスキップ
                        if (tmp.Substring(0, 2) == "//") continue;

                        val.Add(tmp.Split(',').ToList());
                    }
                }
            }
            catch { }
            return val;
        }

        public static void Write(string fileName, List<List<string>> data)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                using (StreamWriter sr = new StreamWriter(fs))
                {
                    data.ForEach(p => 
                    {
                        string s="";
                        p.ForEach(q => s += q + ", ");
                        sr.WriteLine(s);
                    });
                }
            }
            catch { }
        }

        public static bool[,] GetMapData(string fileName, int width, int height)
        {
            var result = new bool[width,height];
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open))
                using(var sr = new StreamReader(fs))
                {
                    var count = 0;
                    while (!sr.EndOfStream)
                    {
                        var tmp = sr.ReadLine();
                        var tmpData = tmp.ToCharArray();
                        for (int i = 0; i < width; i++)
                        {
                            result[i, count] = (tmpData[i] == '1');
                        }
                        count++;
                    }

                }
            }
            catch { Console.WriteLine("false!"); }
            return result;
        }
    }
}
