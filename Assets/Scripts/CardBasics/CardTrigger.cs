using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardTrigger : MonoBehaviour
{
    public UnityEvent OnChange;
    // Start is called before the first frame update
    void InvokeOnChange()
    {
        OnChange?.Invoke();
    }
}
