using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class BrushOptionSetTileButton : MonoBehaviour, IPointerClickHandler
{
    private byte tileID = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        Tools.BrushTool.tileID = this.tileID;
    }

    public void SetUp(byte ID)
    {
        tileID = ID;
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        //props.SetFloat("_Index", tileID);
        var image = gameObject.GetComponent<Image>();
        var mat = Instantiate(image.material);
        mat.SetInteger("_Index", tileID);
        image.material = mat;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
