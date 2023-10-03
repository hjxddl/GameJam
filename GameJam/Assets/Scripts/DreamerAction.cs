using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DreamerAction : MonoBehaviour
{
    public static event Action DreamerDieEvent;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            Debug.Log("有效带");
            
            DreamerDieEvent();
        }
    }

}
