using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools
{
    public class PanTool : Tool
    {
        bool running = false;

        EventSystem es;

        Camera mainCamera;

        // Use this for initialization
        void Start()
        {
            es = EventSystem.current;
            mainCamera = Camera.main;
        }

        IEnumerator Pan()
        {
            Vector2 previousLocation = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            yield return null;
            while (Input.GetMouseButton(0))
            {
                Vector2 newLocation = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                mainCamera.transform.Translate(previousLocation - newLocation);
                previousLocation = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                yield return null;
            }
            running = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (es.IsPointerOverGameObject() ||
                running) return;

            if (Input.GetMouseButtonDown(0))
            {
                running = true;
                StartCoroutine(Pan());
            }
        }
    }
}