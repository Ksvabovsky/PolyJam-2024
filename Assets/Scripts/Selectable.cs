using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public virtual void HighlightMe() { }

    public virtual void DeHighlightMe() { }

}
