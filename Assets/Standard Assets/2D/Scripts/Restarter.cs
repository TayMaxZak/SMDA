using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
        [SerializeField]
        private Vector3 respawnPos;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                other.transform.position = respawnPos;
                //SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
            }
        }
    }
}
