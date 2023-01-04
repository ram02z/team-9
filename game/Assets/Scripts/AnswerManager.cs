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
        if (quizManager.isTimerOn)
        {
            Debug.LogError("Can't select option whilst timer is on");
            return;
        }

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
