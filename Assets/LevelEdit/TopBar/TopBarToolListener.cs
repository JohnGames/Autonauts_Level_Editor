using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Tools;

public class TopBarToolListener : MonoBehaviour, IPointerClickHandler
{
    public ToolEnum toolEnum;
    public Image button;
    public Image icon;
    public Color ButtonNotSet;
    public Color ButtonSet;
    public Color IconNotSet;
    public Color IconSet;

    public void OnPointerClick(PointerEventData eventData)
    {
        ToolsManager.SetTool(toolEnum);
    }

    private void CheckIfSet()
    {
        if (ToolsManager.GetTool() == toolEnum)
        {
            button.color = ButtonSet;
            icon.color = IconSet;
            return;
        }

        button.color = ButtonNotSet;
        icon.color = IconNotSet;

    }

    private void OnEnable()
    {
        ToolsManager.ToolChanged += CheckIfSet;
        CheckIfSet();
    }

    private void OnDisable()
    {
        ToolsManager.ToolChanged -= CheckIfSet;
    }
}
