using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class KickRadiusInteract : InteractableBase
{
    public event EventHandler OnKickStart;
    public event EventHandler OnKickEnd;


    public event EventHandler<OnKickArrowDirectionEventArgs> OnKickArrowDirection;
    public class OnKickArrowDirectionEventArgs : EventArgs
    {
        public float angle;
    }
    
    public List<IKickable> kickableList = new List<IKickable>();

    [SerializeField] private float kickVelocity = 10f;
    [SerializeField] private float drag = 20f;
    [SerializeField] private float holdTimeScale = .5f;
    [SerializeField] private float kickHoldTime = 1f;
    [SerializeField] private float startDelayTime;

    private Vector2 kickDirection;
    [SerializeField] private Vector2 kickDirectionInput;

    private bool isKickKey;
    private bool isKickKeyDown;

    private bool isHolding;
    private bool isInteractable;


    private void Start() 
    {
        InputManager.Instance.OnKick += InputManager_OnKick;
    }
    
    // 瞄準時緩
    private void InputManager_OnKick(object sender, EventArgs e)
    {
        if(isInteractable)
        {
            isHolding = true;
            Time.timeScale = holdTimeScale;
            startDelayTime = Time.unscaledTime;

            OnKickStart?.Invoke(this, EventArgs.Empty);  // 顯示進入範圍
        }
    }

    private void Update()
    {
        isKickKey = InputManager.Instance.IsKickKey();
        isKickKeyDown = InputManager.Instance.IsKickKeyDown();
        kickDirectionInput = InputManager.Instance.KickDirectionInput();
        
        if(isInteractable)  Interact();
    }

    public override void Interact()
    {
        if(isHolding)
        {
            if(kickDirectionInput != Vector2.zero)
            {
                kickDirection = kickDirectionInput;
                kickDirection.Normalize();
            }
            Debug.Log(kickDirection);

            // 瞄準箭頭角度
            float _angle = Vector2.SignedAngle(Vector2.right, kickDirection);
            OnKickArrowDirection.Invoke(this, new OnKickArrowDirectionEventArgs
            {
                angle = _angle
            });
            
            StartKick();
        }  
    }

    private void StartKick()
    {
        if(!isKickKey || Time.unscaledTime >= startDelayTime + kickHoldTime)
        {
            isHolding = false;
            Time.timeScale = 1f;
            startDelayTime = Time.time;

            // add velocity
            foreach(IKickable _kickable in kickableList)
            {
                _kickable.AddForce(kickDirection * kickVelocity);
            }

            OnKickEnd?.Invoke(this, EventArgs.Empty);   // // 取消顯示範圍
        }
    }

    #region OnTrigger
    protected override void OnTriggerEnter2D(Collider2D _other)
    {
        IKickable _kickable = _other.GetComponent<IKickable>();
        if(_kickable == null)   return;

        isInteractable = true;

        kickableList.Add(_kickable);
        foreach (IKickable _ikickable in kickableList)
        {
            _ikickable.EnterRadius();
        }
    }
    protected override void OnTriggerExit2D(Collider2D _other)
    {
        IKickable _kickable = _other.GetComponent<IKickable>();
        if(_kickable == null) return;

        kickableList.Remove(_kickable);
        foreach (IKickable _ikickable in kickableList)
        {
            OnKickEnd.Invoke(this, EventArgs.Empty);
            _ikickable.ExitRadius();
        }

        if (kickableList.Count == 0)
        {
            isInteractable = false;
            Time.timeScale = 1f;
        }
    }
    #endregion
}
