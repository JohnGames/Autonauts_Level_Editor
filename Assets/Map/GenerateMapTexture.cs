using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldInfo;

[RequireComponent(typeof(MeshRenderer))]
public class GenerateMapTexture : MonoBehaviour
{

    private Texture2D tex;

    private void OnEnable()
    {
        World.TilesSet += World_TilesSet;
    }

    private void World_TilesSet(List<World.MapTiles> tiles)
    {
        foreach (var item in tiles)
        {
            Color32 c = new Color32(item.type, 0, 0, 0);
            tex.SetPixel(item.x, item.y, c);
        }
        tex.Apply();
    }

    private void OnDisable()
    {
        World.TilesSet -= World_TilesSet;
    }

    private void Start()
    {
        if (World.GetMapHeight == 0 ||
            World.GetMapWidth == 0) return;

        var mapTiles = World.GetMapTiles;

        if (mapTiles == null) return;

        RenderMap(mapTiles);
    }

    public void RenderMap(byte[,] tiles)
    {
        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);

        transform.localScale = new Vector3(width, height, 1f);

        tex = new Texture2D(width, height, TextureFormat.R8, false);
        tex.filterMode = FilterMode.Point;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                byte type = tiles[x, y];
                Color32 c = new Color32(type, 0, 0, 0);
                tex.SetPixel(x, y, c);
            }
        }

        tex.Apply();

       
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetTexture("_SplatMap", tex);
        gameObject.GetComponent<MeshRenderer>().SetPropertyBlock(props);
    }
}
