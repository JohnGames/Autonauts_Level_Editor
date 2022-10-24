using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public enum SceneEnum
    {
        MainMenu = 0,
        LevelEdit = 1
    }

    static public class Scene
    {
        static public void Change(SceneEnum sceneEnum)
        {
            SceneManager.LoadScene(((int)sceneEnum), LoadSceneMode.Single);
        }
    }
}