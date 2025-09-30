using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // ชื่อ Scene ที่จะโหลด (เช่น EndCredit)
    public string sceneToLoad = "EndCredit";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player แตะประตู!");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
