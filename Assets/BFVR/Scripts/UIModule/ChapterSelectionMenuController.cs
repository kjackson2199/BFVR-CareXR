using BFVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSelectionMenuController : MonoBehaviour
{
    public void LoadMainMenu()
    {
        BFVRApp.LoadMainMenuScene();
    }

    public void LoadChapterByName(string name)
    {
        BFVRApp.LoadSceneByName(name);
    }
}
