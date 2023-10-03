using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set;}

    public event EventHandler OnKick;

    private PlayerInput playerInput;

    [SerializeField] private float holdTime = .1f;
    [SerializeField] private KickRadiusInteract kickRadiusInteract;

    private Vector2 kickDirection_Raw;
    private Vector2Int kickDirectionInput;

    private bool isKickKey;
    private bool isKickKeyDown;
    private bool isPauseKey;
    private bool isPauseKeyDown;

    private Coroutine holdTimeCoroutine;
    
    [SerializeField] private Camera cam;

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }

        playerInput = new PlayerInput();    
        playerInput.Player.Enable();
        playerInput.UI.Enable();
        playerInput.Pause.Disable();

        playerInput.Player.Kick.performed +=  Kick_started;
        playerInput.Player.Kick.canceled += Kick_canceled;
        playerInput.Player.Pause.performed += Pause_started;
    }
    private void OnDestroy() 
    {
        playerInput.Player.Kick.performed -=  Kick_started;
        playerInput.Player.Kick.canceled -= Kick_canceled;      

        playerInput.Dispose();      
    }

    #region Kick
    private void Kick_started(InputAction.CallbackContext context)
    {
        isKickKey = true;
        isKickKeyDown = true;
        OnKick.Invoke(this, EventArgs.Empty);

        if(holdTimeCoroutine == null)
        {
            holdTimeCoroutine = StartCoroutine(Coroutine_KickHold());
        }
        else
        {
            holdTimeCoroutine = null;
            holdTimeCoroutine = StartCoroutine(Coroutine_KickHold());
        }
    }

    private void Kick_canceled(InputAction.CallbackContext context)
    {
        isKickKey = false;
        isKickKeyDown = false;
    }

    private void Pause_started(InputAction.CallbackContext context)
    {
        isPauseKey = true;
        isPauseKeyDown = true;
    }

    private IEnumerator Coroutine_KickHold()
    {
        WaitForSeconds wait = new WaitForSeconds(holdTime);

        yield return wait;
        isKickKeyDown = false;
    }
    #endregion 

    #region  KickDirection
    public Vector2 KickDirection_Raw()
    {
        kickDirection_Raw = playerInput.Player.KickDirection.ReadValue<Vector2>();

        kickDirection_Raw = cam.ScreenToWorldPoint((Vector3)kickDirection_Raw) - kickRadiusInteract.gameObject.transform.position;

        return kickDirection_Raw;         
    }
    public Vector2Int KickDirectionInput()
    {
        kickDirection_Raw = playerInput.Player.KickDirection.ReadValue<Vector2>();

        kickDirection_Raw = cam.ScreenToWorldPoint((Vector3)kickDirection_Raw) - kickRadiusInteract.gameObject.transform.position;

        kickDirectionInput = Vector2Int.RoundToInt(kickDirection_Raw.normalized);

        return kickDirectionInput;
    }
    #endregion

    /* Get Key */
    public bool IsKickKeyDown()
    {
        bool _result = isKickKeyDown;
        isKickKeyDown = false;

        return _result;
    }
    public bool IsKickKey()
    {
        return isKickKey;
    }
    
    public bool IsPauseKey()
    {
        return isPauseKey; 
    }
    public bool IsPauseKeyDown() {
        return isPauseKeyDown;
    }
    public void SwitchCurrentActionMap(string A)
    {
        if (A == "Pause")
        {
            playerInput.Player.Disable();
            playerInput.Pause.Enable();
        }
        else
        {
            playerInput.Player.Enable();
            playerInput.Pause.Disable();
        }
    }
}
