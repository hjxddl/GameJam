using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class InteractableBase : MonoBehaviour
{
    private const string Player = "Player";

    [SerializeField] protected CircleCollider2D coll;
    [SerializeField] protected GameObject icon;

    public abstract void Interact();
    private void InputManager_OnInteract_Performed(object sender, EventArgs e)
    {
        Interact();
    }

    /* 玩家是否在範圍內 */
    #region OnTrigger
    protected virtual void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.gameObject.tag == Player)
        {
            //icon.SetActive(true);

            //InputManager.Instance.OnInteract_Performed += InputManager_OnInteract_Performed;
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D _other) 
    {
        if(_other.gameObject.tag == Player)
        {
            //icon.SetActive(false);

            //InputManager.Instance.OnInteract_Performed -= InputManager_OnInteract_Performed;
        }
    }
    #endregion
}
