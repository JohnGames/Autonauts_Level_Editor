using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace WorldInfo
{
    internal class WorldData
    {
        private JSONNode data;
        internal WorldData(string data)
        {
            this.data = JSONNode.Parse(data);
        }

        /// <summary>
        /// Checks file, breaking at first issue found.
        /// </summary>
        /// <param name="issue"></param>
        /// <returns>False if no issue, True if problem.</returns>
        internal bool Validate(out string issue)
        {
            issue = "";
            if (data == null)
            {
                issue = "No data found, are you loading a valid JSON file?";
                return true;
            }
            if (data["Tiles"] == null)
            {
                issue = "No tile data found.";
                return true;
            }
            if (mapWidth == 0)
            {
                issue = "Map width cannot be 0.";
                return true;
            };
            if (mapHeight == 0)
            {
                issue = "Map height cannot be 0.";
                return true;
            }
            return true;
        }

        public int mapWidth
        {
            get
            {
                return data["Tiles"]["TilesWide"].AsInt;
            }
        }

        public int mapHeight
        {
            get
            {
                return data["Tiles"]["TilesHigh"].AsInt;
            }
        }

        internal string Save()
        {
            data["Tiles"]["TileTypes"] = null;



            //Reminder: store the top left as 0,0. We need to conver from bottom left as 0,0.

            List<int> iarray = new List<int>();

            int y = mapHeight - 1;
            int x = 0;

            byte storedType = mapTiles[x, y];
            int count = 0;

            for (; y >= 0; y--)
            {
                for (; x < mapWidth; x++)
                {
                    var tileType = mapTiles[x, y];
                    if (tileType == storedType)
                    {
                        count += 1;
                    }
                    else
                    {
                        iarray.Add(storedType);
                        iarray.Add(count);
                        storedType = tileType;
                        count = 1;
                    }
                }
                x = 0;
            }

            iarray.Add(storedType);
            iarray.Add(count);

            data["Tiles"].Remove("TileTypes");

            for (int i = 0; i < iarray.Count; i++)
            {
                data["Tiles"]["TileTypes"][i].AsInt = iarray[i];
            }

            return data.ToString();
        }

        private byte[,] mapTiles = null;

        internal bool TrySetTile(int x, int y, byte b)
        {
            if (x < 0 ||
                   x > mapWidth - 1 ||
                   y < 0 ||
                   y > mapHeight - 1) return false;

            if (GetMapTiles == null) return false;

            if (mapTiles[x, y] == b) return false;

            mapTiles[x, y] = b;

            return true;
        }

        public byte[,] GetMapTiles
        {
            get
            {
                if (data == null) return null;
                if (mapTiles != null) return mapTiles;

                int width = mapWidth;
                int height = mapHeight;

                mapTiles = new byte[width, height];

                //He stores it wehre top left is 0,0. Converting to bottom left is 0,0
                int x = 0;
                int y = height - 1;

                for (int i = 0; i < data["Tiles"]["TileTypes"].Count; i += 2)
                {
                    byte type = (byte)data["Tiles"]["TileTypes"][i].AsInt;
                    int count = data["Tiles"]["TileTypes"][i + 1].AsInt;

                    for (int j = 0; j < count; j++)
                    {
                        mapTiles[x, y] = type;
                        x += 1;
                        if (x == width)
                        {
                            x = 0;
                            y -= 1;
                        }
                    }
                }

                return mapTiles;
            }
        }
    }
}