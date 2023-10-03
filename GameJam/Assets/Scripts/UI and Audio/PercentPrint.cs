using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentPrint : MonoBehaviour
{
    public Slider percent;
    public Text text;
    public void ChangePercent()
    {
        
        text.text =string.Format("{0:N0}%",percent.value*100);
    }
}
