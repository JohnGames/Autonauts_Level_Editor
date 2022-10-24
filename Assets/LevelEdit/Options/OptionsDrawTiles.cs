using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsDrawTiles : MonoBehaviour
{
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= byte.MaxValue; i++)
        {
            GameObject go = GameObject.Instantiate(button, transform);
            go.GetComponent<BrushOptionSetTileButton>().SetUp((byte)i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
