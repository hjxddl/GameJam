using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{   
    
    public AudioSource BGM,SE;//音源组件
    public Slider MainVolume1,BGM1,SE1;//主音效,背景音乐,游戏音效的滑块组件
    float PriBGM,PriSE,PriMainVolume1,PriBGM1,PriSE1;//保存初始值
    

    public void ChangeMainVolume()
    {
        BGM.volume= MainVolume1.value*BGM1.value;
        SE.volume = MainVolume1.value * SE1.value;
    }
    public void ChangeBGM()
    {
        BGM.volume = MainVolume1.value *BGM1.value;
    }
    public void ChangeSe()
    {
        SE.volume = MainVolume1.value * SE1.value;
    }
    public void Apply()
    {
        PriBGM = BGM.volume;
        PriSE = SE.volume;
        PriBGM1 = BGM1.value;
        PriSE1 = SE1.value;
        PriMainVolume1 = MainVolume1.value;
    }
    public void Exit()
    {
        BGM.volume = PriBGM;
        SE.volume = PriSE;
        BGM1.value = PriBGM1;
        SE1.value = PriSE1;
        MainVolume1.value = PriMainVolume1;
    }
    void Awake()
    {
        BGM = GetComponents<AudioSource>()[0];
        SE= GetComponents<AudioSource>()[1];
        if (PlayerPrefs.GetFloat("BGM")!=0.7&& PlayerPrefs.GetFloat("SE")!=0.7)
        {
            
            BGM.volume=PlayerPrefs.GetFloat("BGM");
            SE.volume = PlayerPrefs.GetFloat("SE");
            MainVolume1.value = PlayerPrefs.GetFloat("MainVolume1");
            BGM1.value = PlayerPrefs.GetFloat("BGM1");
            SE1.value = PlayerPrefs.GetFloat("SE1");
        }
        else
        {
            BGM.volume = 0.7f;
            SE.volume = 0.7f;
        }
        Apply();
        ChangeMainVolume();
        ChangeBGM();
        ChangeSe();
        
    }

}
