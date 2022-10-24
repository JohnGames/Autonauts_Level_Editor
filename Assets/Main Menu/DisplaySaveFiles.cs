using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DisplaySaveFiles : MonoBehaviour
{
    public GameObject FileLoadHolderPrefab;
    public TMPro.TMP_InputField input;

    // Start is called before the first frame update
    void Start()
    {
        if (FileLoadHolderPrefab == null) return;

        string s = Application.persistentDataPath;
        DirectoryInfo di = new DirectoryInfo(s);
        di = di.Parent.Parent;
        s = di.FullName + "/Denki Ltd/Autonauts/Saves/";
        input.text = s;
        if (!Directory.Exists(s)) return;
        di = new DirectoryInfo(s);
        foreach (var item in di.GetDirectories())
        {
            string path = item.FullName + "/World.txt";
            string labelText = item.Name;

            byte[] thumbnailData = File.ReadAllBytes(item.FullName + "/Thumbnail.jpg");
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(thumbnailData);
            Rect rec = new Rect(0, 0, tex.width, tex.height);
            Sprite sprite = Sprite.Create(tex, rec, new Vector2(0, 0), 0.1f);
            
            var go = GameObject.Instantiate(FileLoadHolderPrefab, transform);
            go.GetComponent<FileLoadHolder>().Setup(path, sprite, labelText);

        }
    }

}
