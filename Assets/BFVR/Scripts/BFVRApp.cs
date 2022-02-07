using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BFVR
{
    /// <summary>
    /// Standard BFVR App Manager. Manages base functionality of the app: Scene Loading, Gameobject whitelisting, UI Canvases, and Audio Cueing. Place in first scene that loads.
    /// </summary>
    public class BFVRApp : MonoBehaviour
    {
        public delegate void OnBeginLoadSceneDelegate();
        public static event OnBeginLoadSceneDelegate onBeginLoadSceneEvent;
        public delegate void OnCompleteLoadSceneDelegate();
        public static event OnCompleteLoadSceneDelegate onCompleteLoadSceneEvent;

        private static BFVRApp _instance;
        public static BFVRApp Instance { get { return _instance; } }

        [Header("Scene Management")]
        public string MainMenuSceneName;
        public string LoadingSceneName;

        public List<string> Scenes;

        [HideInInspector] [Space] [Range(0, 1.5f)] public float AsyncLoadDelayTime = 1;

        [Space]
        [Tooltip("Game Objects to not destroy when loading.")]
        public List<GameObject> gameObjectWhitelist;

        private void Awake()
        {
            if(_instance && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            // Add self to don't destroy list
            DontDestroyOnLoad(gameObject);

            foreach(GameObject g in gameObjectWhitelist)
            {
                if (!g) continue;
                DontDestroyOnLoad(g);
            }
        }

        #region Scene Management
        public static void LoadMainMenuScene()
        {
            SceneManager.LoadScene(_instance.MainMenuSceneName);
        }

        public static void LoadLoadingScene()
        {
            SceneManager.LoadScene(_instance.LoadingSceneName);
        }

        public static void LoadSceneByName(string sceneName)
        {
            if(sceneName == _instance.MainMenuSceneName)
            {
                LoadMainMenuScene();
            }
            else if(sceneName == _instance.LoadingSceneName)
            {
                return;
            }
            else
            {
                foreach(string s in _instance.Scenes)
                {
                    if(s == sceneName)
                    {
                        SceneManager.LoadScene(sceneName);
                    }
                }
            }

        }

        IEnumerator LoadSceneAsync(string sceneName)
        {
            onBeginLoadSceneEvent.Invoke();

            yield return new WaitForSeconds(_instance.AsyncLoadDelayTime);

            LoadLoadingScene();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            asyncLoad.allowSceneActivation = true;

            while(!asyncLoad.isDone)
            {
                yield return null;
            }

            onCompleteLoadSceneEvent.Invoke();
        }
        #endregion
    }
}