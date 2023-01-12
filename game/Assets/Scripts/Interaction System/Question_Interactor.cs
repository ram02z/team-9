using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

//A script that preforms an action when an interaction happens
public class Question_Interactor : NetworkBehaviour, I_Interactable
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
    private GamePlayer lastInteractor = null;

    public bool Interact(Interactor interactor)
    {
        lastInteractor = interactor.GetComponent<GamePlayer>();
        FreezeInteractor(interactor);
        quizManager.quizCanvas.SetActive(true);
        if (isCorrect)
        {
            ShowCodePanel();
        }
        else if (isCooldown)
        {
            ShowCooldownPanel();
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

    private void FreezeInteractor(Interactor interactor)
    {
        var movementController = interactor.GetComponent<PlayerMovementController>();
        var cameraController = interactor.GetComponent<PlayerCameraController>();
        movementController.SetCanMove(false);
        cameraController.SetCanMove(false);
        quizManager.closeButton.onClick.AddListener(() =>
        {
            quizManager.quizCanvas.SetActive(false);
            movementController.SetCanMove(true);
            cameraController.SetCanMove(true);
        });

    }

    private void ShowCodePanel()
    {
        quizManager.quizPanel.SetActive(false);
        quizManager.answerPanel.SetActive(true);
        quizManager.codePanel.SetActive(true);
        quizManager.cooldownPanel.SetActive(false);
    }

    private void ShowCooldownPanel()
    {

        quizManager.quizPanel.SetActive(false);
        quizManager.answerPanel.SetActive(true);
        quizManager.codePanel.SetActive(false);
        quizManager.cooldownPanel.SetActive(true);
    }

    /// <summary>
    /// Move to the next question
    /// </summary>
    public void Correct()
    {
        lastInteractor.score += 1;
        isCorrect = true;
        quizManager.codeText.text = $"The code is {Code}";
        ShowCodePanel();
    }

    public void Wrong()
    {
        isCooldown = true;
        StartCoroutine(Cooldown());
        ShowCooldownPanel();
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
