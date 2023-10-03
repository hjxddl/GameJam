using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanKick : MonoBehaviour, IKickable
{
    [SerializeField] private KickRadiusInteract kickRadiusInteract;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform kickDirectionArrow; 

    #region Event
    public void EnterRadius() 
    {
        kickRadiusInteract.OnKickStart += kickRadiusInteract_OnKickStart;
        kickRadiusInteract.OnKickEnd += kickRadiusInteract_OnKickEnd;
    
        kickRadiusInteract.OnKickArrowDirection += kickRadiusInteract_OnKickArrowDirection;
    }
    public void ExitRadius() 
    {
        kickRadiusInteract.OnKickStart -= kickRadiusInteract_OnKickStart;
        kickRadiusInteract.OnKickEnd -= kickRadiusInteract_OnKickEnd;
    
        kickRadiusInteract.OnKickArrowDirection -= kickRadiusInteract_OnKickArrowDirection;
    }
    private void kickRadiusInteract_OnKickStart(object sender, EventArgs e)
    {
        kickDirectionArrow.gameObject.SetActive(true);
    }
    private void kickRadiusInteract_OnKickEnd(object sender, EventArgs e)
    {
        kickDirectionArrow.gameObject.SetActive(false);

        kickDirectionArrow.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    private void kickRadiusInteract_OnKickArrowDirection(object sender, KickRadiusInteract.OnKickArrowDirectionEventArgs e)
    {
        kickDirectionArrow.rotation = Quaternion.Euler(0f, 0f, e.angle - 45f);

        //Debug.Log(e.angle);
    }

    #endregion

    public void SetVelocity(Vector2 _velocity) 
    {
        rb.velocity = _velocity;
    }

    public void AddForce(Vector2 _Force)
    {
        rb.AddForce(_Force, ForceMode2D.Impulse);
    }

    public void SetDrag(float _drag)
    {
        rb.drag = _drag;
    }
}
