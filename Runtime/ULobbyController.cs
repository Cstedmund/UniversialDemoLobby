using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace UniversialLobby {
    public class ULobbyController : MonoBehaviour {
        [SerializeField]
        private GameObject m_ButtonHolder, m_ButtonToSpawn;
        //private ULobbyManager _uLobbyManager;

        void Start() {
            //_uLobbyManager = ULobbyManager.Instance;
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            for (var i = 1; i < sceneCount; i++) {
                var tempBrn = Instantiate(m_ButtonToSpawn, m_ButtonHolder.transform);
                var buildIndex = i;
                tempBrn.transform.GetChild(0).GetComponent<Text>().text = GetSceneNameByBuildIndex(buildIndex);
                tempBrn.GetComponent<Button>().onClick.AddListener(() => { var _ = StartCoroutine(LoadSceneAsync(buildIndex)); });
            }
            m_ButtonHolder.transform.GetChild(0).GetComponent<Button>().interactable = false;
        }

        private string GetSceneNameByBuildIndex(int buildIndex) {
            var path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            return Path.GetFileNameWithoutExtension(path);
        }

        private IEnumerator LoadSceneAsync(int buildIndex) {
            Debug.Log(buildIndex);
            var sceneLoadReq = SceneManager.LoadSceneAsync(buildIndex);
            yield return new WaitUntil(() => sceneLoadReq.isDone);
        }
    }
}

