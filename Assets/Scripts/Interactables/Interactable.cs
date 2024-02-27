using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //message for player when looking at interactable 
    public string promptMessage;
    public void BaseInteract()
    {
        Interact(); 
    }
    protected virtual void Interact()
    {






    }

}
