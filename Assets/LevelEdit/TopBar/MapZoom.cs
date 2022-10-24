using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MapZoom : MonoBehaviour
{

    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        var slider = gameObject.GetComponent<Slider>();
        slider.onValueChanged.AddListener(ValueChange);
        ValueChange(slider.value);
    }

    public void ValueChange(float value)
    {
        mainCamera.orthographicSize = value;
    }
}
