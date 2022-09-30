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
        private ULobbyManager _uLobbyManager;

        void Start() {
            _uLobbyManager = ULobbyManager.Instance;
            var sceneCount = SceneManager.sceneCountInBuildSettings;

            for (var i = 1; i < sceneCount; i++) {
                var tempBrn = Instantiate(m_ButtonToSpawn, m_ButtonHolder.transform);
                tempBrn.transform.GetChild(0).GetComponent<Text>().text = GetSceneNameByBuildIndex(i);
                //tempBrn.GetComponent<Button>().onClick.AddListener(() => { var _ = StartCoroutine(LoadSceneAsync(idx)); });
                tempBrn.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)delegate { SelectScenenLoad(tempBrn);});
                //var trigger = tempBrn.GetComponent<EventTrigger>();
                //var entry = new EventTrigger.Entry();
                //entry.eventID = EventTriggerType.PointerUp;
                //entry.callback.AddListener(data => { SetCurrentBook(_bookItem); });
                //trigger.triggers.Add(entry);
            }
            m_ButtonHolder.transform.GetChild(0).GetComponent<Button>().interactable = false;
        }

        private void SelectScenenLoad(GameObject obj) {
            _uLobbyManager.InternalLoadScene(obj.transform.GetChild(0).GetComponent<Text>().text);
        }

        private string GetSceneNameByBuildIndex(int buildIndex) {
            var path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            return Path.GetFileNameWithoutExtension(path);
        }

        public IEnumerator LoadSceneAsync(int buildIndex) {
            Debug.Log(buildIndex);
            var sceneLoadReq = SceneManager.LoadSceneAsync(buildIndex);
            yield return new WaitUntil(() => sceneLoadReq.isDone);
        }
    }
}

