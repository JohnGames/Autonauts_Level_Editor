using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

public class TestSave : MonoBehaviour
{

    public TextAsset testFile;

    private JSONNode data;

    // Start is called before the first frame update
    void Start()
    {
        string s = testFile.ToString();
        Debug.Log(s);
        data = JSONNode.Parse(s);
        Debug.Log(data.ToString());
    }

}
