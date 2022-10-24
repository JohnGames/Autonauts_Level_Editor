using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldInfo;
using UnityEngine.EventSystems;

namespace Tools
{
    public class BrushTool : Tool
    {
        private LayerMask mapMask;

        public int x = 0;
        public int y = 0;
        public int width = 0;
        public int height = 0;

        public static byte tileID = 0;

        private EventSystem es;

        public GameObject toolOptions;

        private void GetDimensions()
        {
            width = World.GetMapWidth;
            height = World.GetMapHeight;
        }

        private void Start()
        {
            GetDimensions();
            es = EventSystem.current;
            mapMask = LayerMask.GetMask("Map");
        }

        // Update is called once per frame
        void Update()
        {
            if (es.IsPointerOverGameObject()) return;

            if (!Input.GetMouseButton(0)) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, 150f, mapMask)) return;

            x = Mathf.FloorToInt(hit.textureCoord.x * width);
            y = Mathf.FloorToInt(hit.textureCoord.y * height);

            List<World.MapTiles> mapTiles = new List<World.MapTiles>();
            mapTiles.Add(new World.MapTiles(x, y, tileID));
            World.SetTiles(mapTiles);
        }
    }
}