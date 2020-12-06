using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
             A,B,C,D,E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform SpawnPoint;
        [SerializeField] DestinationIdentifier destinationIdentifier;
        [SerializeField] float fadeOutTime = 1f, FadeInTime = 2f, FadeWaitTime = 0.5f;
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

            if(sceneToLoad == -1)
            {
                Debug.Log("Invalid sceneToLoad Index");
                yield break;
            }

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            yield return new WaitForSeconds(FadeWaitTime);
            yield return fader.FadeIn(FadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.SpawnPoint.position);
            player.transform.rotation = otherPortal.SpawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destinationIdentifier == this.destinationIdentifier) continue;
                
                else return portal;

            }

            return null;
        }
    }

}