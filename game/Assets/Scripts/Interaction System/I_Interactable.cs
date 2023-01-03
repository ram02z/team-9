using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Genral Interface class that signals if an interaction has happened
//(Developed in case multiple interaction types are developed)
public interface I_Interactable
{
    //Gets type of interaction action
    public string InteractionPrompt { get; }

    //Signal for if interaction has happened
    public bool Interact(Interactor interactor);
    
}
