using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public abstract class Tool : MonoBehaviour {
        public GameObject optionsPanelContent;
    };

    public enum ToolEnum
    {
        None,
        Pan,
        Brush
    }

    public static class ToolsManager
    {

        private static GameObject holder;

        private static Tool currentTool;

        public static event Action ToolChanged;

        private static ToolEnum currentToolEnum = ToolEnum.None;

        private static GameObject Holder
        {
            get
            {
                if (holder == null)
                {
                    holder = new GameObject("ToolManagerHolder");
                }

                return holder;
            }
        }

        public static ToolEnum GetTool()
        {
            //Sanity Check
            if (holder == null)
            {
                ToolEnum toolEnum = currentToolEnum;
                SetTool(ToolEnum.None);
                SetTool(toolEnum);
            }
            return currentToolEnum;
        }

        public static void SetTool<T>()
            where T : Tool
        {
            if (typeof(T) == currentTool.GetType()) return;

            if(currentTool != null)
            {
                GameObject.Destroy(currentTool);
            }

            currentTool = Holder.AddComponent<T>();
        }

        public static void SetTool(ToolEnum toolEnum)
        {
            if (currentToolEnum == toolEnum) return;

            Tool tool = null;
            if (currentTool != null)
            {
                GameObject.Destroy(currentTool);
            }

            switch (toolEnum)
            {
                case ToolEnum.Pan:
                    tool = Holder.AddComponent<PanTool>();
                    break;
                case ToolEnum.Brush:
                    tool = Holder.AddComponent<BrushTool>();
                    break;
                default:
                    break;
            }

            currentTool = tool;
            currentToolEnum = toolEnum;
            ToolChanged?.Invoke();
        }
    }
}