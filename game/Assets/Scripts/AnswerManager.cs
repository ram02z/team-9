using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public bool isCorrect;
    public QuizManager quizManager;
    private System.Random _rnd;

    /// <summary>
    /// Handles the behaviour post answer
    /// Called by Unity on click.
    /// </summary>
    public void Answer()
    {
        if (isCorrect)
        {
            quizManager.Correct();
            Debug.Log("Correct Answer");
            // TODO: handle behaviour when codes are exhausted 
            var code = quizManager.codes[0];
            quizManager.codes.RemoveAt(0);
            
            quizManager.answerText.text = $"The code is {code}";
        }
        else
        {
            quizManager.isTimerOn = true;
            Debug.Log("Wrong Answer");
        }
        quizManager.quizPanel.SetActive(false);
        quizManager.answerPanel.SetActive(true);
    }
}
