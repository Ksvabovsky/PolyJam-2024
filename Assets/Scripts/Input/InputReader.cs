using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour,Controls.IBasicActions
{
    public static InputReader instance { get; private set; }

    Controls input;

    public Action LeftClick;
    public Action LeftClickRelase;
    // Start is called before the first frame update
    void Awake()
    {

        instance = this;
        input = new Controls();
        input.Basic.Enable();
        input.Basic.SetCallbacks(this);
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            LeftClick.Invoke();
            //Debug.Log("Dupa");
        }

        if(LeftClickRelase != null && context.canceled)
        {
            LeftClickRelase.Invoke();
            //Debug.Log("jebana");
        }
    }

    public void OnRightClick(InputAction.CallbackContext context) {
    

    }
}
