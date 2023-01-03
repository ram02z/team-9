using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A script that peforms an action when an interaction happens
public class Question_Interactor : MonoBehaviour, I_Interactable
{

    [SerializeField] private string _promt;
    public string InteractionPrompt => _promt;

    public bool Interact(Interactor interactor)
    {
        //TODO: Implement question to open
        Debug.Log("Opening question");
        return true;
    }
}
