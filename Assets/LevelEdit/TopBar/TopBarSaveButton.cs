using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TopBarSaveButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        WorldInfo.World.Save();

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
