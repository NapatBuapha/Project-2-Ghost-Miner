using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    // ฟังก์ชันเมื่อกดปุ่ม Play
    public void PlayGame()
    {
        // โหลด Scene Gameplay (อย่าลืมไปใส่ใน Build Settings)
        SceneManager.LoadScene("GameScene");
    }

    // ฟังก์ชันเมื่อกดปุ่ม Option
    public void OpenOptions()
    {
        Debug.Log("Open Options (ใส่ระบบเองภายหลัง)");
    }

    // ฟังก์ชันเมื่อกดปุ่ม Quit
    public void QuitGame()
    {
        Debug.Log("Quit Game"); 
        Application.Quit();
    }
}
