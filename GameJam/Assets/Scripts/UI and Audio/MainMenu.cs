using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource BGM, SE;//音源组件
    public Slider MainVolume1, BGM1, SE1;//主音效,背景音乐,游戏音效的滑块组件
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is quited");
    }
    public void StartGame()
    {
        PlayerPrefs.SetFloat("BGM", BGM.volume);
        PlayerPrefs.SetFloat("SE", SE.volume);
        PlayerPrefs.SetFloat("BGM1", BGM1.value);
        PlayerPrefs.SetFloat("SE1", SE1.value);
        PlayerPrefs.SetFloat("MainVolume1", MainVolume1.value);
        SceneManager.LoadScene("SampleScene");
    }

}
