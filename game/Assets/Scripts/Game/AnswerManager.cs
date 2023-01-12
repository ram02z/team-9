using UnityEngine;
using UnityEngine.Serialization;

public class AnswerManager : MonoBehaviour
{
    public bool isCorrect;
    public Question_Interactor questionInteractor;

    /// <summary>
    /// Handles the behaviour post answer
    /// Called by Unity on click.
    /// </summary>
    public void Answer()
    {
        if (questionInteractor.isCooldown)
        {
            Debug.LogError("On cooldown. Try again later!");
            return;
        }
        if (isCorrect)
        {
            questionInteractor.Correct();
            Debug.Log("Correct Answer");
        }
        else
        {
            questionInteractor.Wrong();
            Debug.Log("Wrong Answer");
        }
    }
}
