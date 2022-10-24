using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace WorldInfo
{
    public static class World
    {
        public class MapTiles
        {
            readonly public int x;
            readonly public int y;
            readonly public byte type;
            public MapTiles(int x, int y, byte type)
            {
                this.x = x;
                this.y = y;
                this.type = type;
            }
        }

        private static WorldData currentWorld;

        public static event Action<List<MapTiles>> TilesSet;

        private static string filePath;

        public static void Save()
        {
            if (currentWorld == null) return;
            File.WriteAllText(filePath, currentWorld.Save());
        }

        public static bool TryLoad(string path)
        {
            if (path == null) return false;
            filePath = path;

            if (!File.Exists(path)) return false;

            currentWorld = new WorldData(File.ReadAllText(path));
            if (!currentWorld.Validate(out string issue))
            {
                Debug.Log(issue);
                return false;
            }

            return true;
        }

        static public int GetMapWidth
        {
            get
            {
                if (currentWorld == null) return 0;
                return currentWorld.mapWidth;
            }
        }

        static public int GetMapHeight
        {
            get
            {
                if (currentWorld == null) return 0;
                return currentWorld.mapHeight;
            }
        }

        static public byte[,] GetMapTiles
        {
            get
            {
                if (currentWorld == null) return null;
                return currentWorld.GetMapTiles;
            }
        }

        static public void SetTiles(List<MapTiles> mapTiles)
        {
            if (currentWorld == null) return;
            List<MapTiles> validTiles = new List<MapTiles>();
            foreach (var item in mapTiles)
            {
                if (currentWorld.TrySetTile(item.x,item.y,item.type)) validTiles.Add(item);
            }

            TilesSet?.Invoke(validTiles);
        }

    }
}