using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using WorldInfo;
using Scenes;

public class FileLoadHolder : MonoBehaviour, IPointerClickHandler
{
    private string path;
    [SerializeField]
    private Image thumbnail;
    [SerializeField]
    private TMP_Text label;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (World.TryLoad(path))
        {
            Scene.Change(SceneEnum.LevelEdit);
        }
    }

    public void Setup(string path, Sprite sprite, string text)
    {
        this.path = path;
        thumbnail.sprite = sprite;
        label.text = text;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
