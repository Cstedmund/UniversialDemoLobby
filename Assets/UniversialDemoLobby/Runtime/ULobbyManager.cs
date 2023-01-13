using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UniversialLobby{
    public class ULobbyManager : MonoBehaviour {
        private static ULobbyManager _instance;
        public static ULobbyManager Instance {
            get { return _instance; }
        }
        private void Awake() {
            if (_instance != null && _instance != this) {
                Destroy(gameObject);
            }
            else {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        [HideInInspector]
        public string currentURL, previousScene;

        public void InternalLoadScene(string sceneName) {
            if ((sceneName == "") & (previousScene != null)) {
                Debug.Log($"Scene load {previousScene}");
                SceneManager.LoadScene(previousScene);
                return;
            }

            Debug.Log($"Scene load {sceneName}");
            SceneManager.LoadScene(sceneName);
        }

    }
}


