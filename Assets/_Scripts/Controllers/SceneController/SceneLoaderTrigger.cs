using System;
using UnityEngine;

public class SceneLoaderTrigger : MonoBehaviour
{
    public static Action loadScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            loadScene();
        }
    }
}
