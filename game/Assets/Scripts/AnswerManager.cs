using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public void Answer()
    {
        quizManager.quizPanel.SetActive(false);
        quizManager.answerPanel.SetActive(true);
        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            quizManager.answerText.text = "The code is XXXX";
            quizManager.Correct();
        }
        else
        {
            quizManager.answerText.text = "Timed out for N seconds";
            Debug.Log("Wrong Answer");
        }
    }
}
