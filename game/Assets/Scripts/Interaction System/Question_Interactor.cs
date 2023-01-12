using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

//A script that preforms an action when an interaction happens
public class Question_Interactor : MonoBehaviour, I_Interactable
{

    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public QuizManager quizManager;
    public Question Question;
    public int Code;
    public bool isCorrect;
    public float timeLeft;
    public bool isCooldown;
    private readonly float _timeoutLength = 10;

    public bool Interact(Interactor interactor)
    {
        quizManager.quizCanvas.SetActive(true);
        if (isCorrect)
        {
            Correct();
        }
        else if (isCooldown)
        {
            quizManager.quizPanel.SetActive(false);
            quizManager.answerPanel.SetActive(true);
            quizManager.codePanel.SetActive(false);
            quizManager.cooldownPanel.SetActive(true);
        }
        else
        {
            quizManager.quizPanel.SetActive(true);
            quizManager.answerPanel.SetActive(false);
            SetQuestionAndAnswers();
        }
        // TODO: lock position and camera
        return true;
    }

    /// <summary>
    /// Move to the next question
    /// </summary>
    public void Correct()
    {
        isCorrect = true;
        quizManager.quizPanel.SetActive(false);
        quizManager.answerPanel.SetActive(true);
        quizManager.codePanel.SetActive(true);
        quizManager.cooldownPanel.SetActive(false);
        quizManager.codeText.text = $"The code is {Code}";
    }

    public void Wrong()
    {
        quizManager.quizPanel.SetActive(false);
        quizManager.answerPanel.SetActive(true);
        quizManager.codePanel.SetActive(false);
        quizManager.cooldownPanel.SetActive(true);
        isCooldown = true;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown() {
        timeLeft = _timeoutLength;
        while (timeLeft > 0)
        {
            quizManager.cooldownText.text = $"{timeLeft}";
            yield return new WaitForSeconds(1);
            timeLeft -= 1;
        }
        isCooldown = false;
    }

    private void SetQuestionAndAnswers()
    {
        quizManager.questionText.text = Question.utterance;
        for (int i = 0; i < quizManager.options.Length; i++)
        {
            var option = quizManager.options[i];
            var answerManager = option.GetComponent<AnswerManager>();
            answerManager.isCorrect = false;
            answerManager.questionInteractor = this;
            option.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Question.choices[i];

            if (Question.answerIndex == i)
            {
                answerManager.isCorrect = true;
            }
        }
    }
}
