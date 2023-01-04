using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//A script that preforms an action when an interaction happens
public class Question_Interactor : MonoBehaviour, I_Interactable
{

    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public GameObject quizCanvas;
    public ThirdPersonMovement thirdPersonMovement;

    public bool Interact(Interactor interactor)
    {
        quizCanvas.SetActive(true);
        thirdPersonMovement.LockPositionAndCamera();
        return true;
    }
}
