using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

public class QuizManager : MonoBehaviour
{
    public List<int> codes;
    public List<Question> questions;
    public float timeLeft;
    public bool isTimerOn;
    public GameObject[] options;
    public Question currentQuestion;
    public TextMeshProUGUI questionText;
    public GameObject quizCanvas;
    public TextMeshProUGUI answerText;
    public GameObject quizPanel;
    public GameObject answerPanel;
    public Button closeButton;
    public ThirdPersonMovement thirdPersonMovement;
    private System.Random _rnd;

    private readonly int _noQuestions = 3;
    private readonly int _noCodes = 4;
    private readonly float _timeoutLength = 10;

    /// <summary>
    /// Initialise the quiz manager
    /// </summary>
    private void Start()
    {
        _rnd = new System.Random();

        // Initialise timer variables
        timeLeft = _timeoutLength;
        isTimerOn = false;

        // Create N 4 digit codes
        codes = Enumerable.Range(1000, 9000).OrderBy(x => _rnd.Next()).Take(_noCodes).ToList();

        // Load questions from JSON file
        using StreamReader r = new StreamReader("questions.json");
        string json = r.ReadToEnd();

        // Initialise quiz
        LoadQuestions(json);
        GenerateQuestion();
        SetupListeners();
    }

    /// <summary>
    /// Update the timer if active
    /// </summary>
    // TODO: move this function to answerManager
    private void Update()
    {
        if (!isTimerOn) return;

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            answerText.text = $"{Mathf.CeilToInt(timeLeft)}";
        }
        else
        {
            isTimerOn = false;
            timeLeft = _timeoutLength;
            answerPanel.SetActive(false);
            quizPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Move to the next question
    /// </summary>
    public void Correct()
    {
        GenerateQuestion();
    }

    /// <summary>
    /// Set answers to GameObjects
    /// </summary>
    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            var option = options[i];
            option.GetComponent<AnswerManager>().isCorrect = false;
            option.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentQuestion.choices[i];

            if (currentQuestion.answerIndex == i)
            {
                option.GetComponent<AnswerManager>().isCorrect = true;
            }
        }
    }

    /// <summary>
    /// Setup event listeners
    /// </summary>
    void SetupListeners()
    {
        closeButton.onClick.AddListener(() =>
        {
            quizCanvas.SetActive(false);
            thirdPersonMovement.UnlockPositionAndCamera();
        });
    }

    /// <summary>
    /// Generate new question if new question exists
    /// </summary>
    private void GenerateQuestion()
    {
        if (questions.Count > 0)
        {
            currentQuestion = questions[0];
            questions.RemoveAt(0);
            questionText.text = currentQuestion.utterance;
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of questions");
        }

    }

    /// <summary>
    /// Load questions from JSON string and log any validation errors
    /// </summary>
    /// <param name="json">JSON string</param>
    private void LoadQuestions(string json)
    {
        questions = new List<Question>();

        List<string> errors = new List<string>();
        questions = JsonConvert.DeserializeObject<List<Question>>(json, new JsonSerializerSettings{
            Error = delegate(object sender, ErrorEventArgs args)
            {
                errors.Add(args.ErrorContext.Error.Message);
                args.ErrorContext.Handled = true;
            },
            Converters = { new IsoDateTimeConverter() }
        });

        if (errors.Any() || questions == null)
        {
            Debug.LogError(errors);
            return;
        }

        var invalidIndex = questions.Any(q => q.answerIndex is < 0 or > 3);

        if (invalidIndex)
        {
            Debug.LogError("JSON contains invalid index. Should be 0 <= x < 4");
            return;
        }

        var invalidChoices = questions.Any(q => q.choices.Length != 4);

        if (invalidChoices)
        {
            Debug.LogError("JSON contains invalid number of choices. Should be 4.");
            return;
        }

        questions = questions.OrderBy(x => _rnd.Next()).Take(_noQuestions).ToList();
    }
}
