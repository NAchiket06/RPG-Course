using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }

        }


        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            //SceneManager.LoadScene(sceneToLoad);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            print("Scene Loaded");
            Destroy(gameObject);
        }
    }

}