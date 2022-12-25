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
    private System.Random _rnd;

    private readonly int _noQuestions = 3;
    private readonly int _noCodes = 4;
    private readonly float _timeoutLength = 10;

    private void Start()
    {
        _rnd = new System.Random();
        timeLeft = _timeoutLength;
        isTimerOn = false;
        codes = Enumerable.Range(1000, 9000).OrderBy(x => _rnd.Next()).Take(_noCodes).ToList();
        using StreamReader r = new StreamReader("questions.json");
        string json = r.ReadToEnd();
        LoadQuestions(json);
        GenerateQuestion();
        SetupListeners();
    }

    // TODO: move this function to answerManager
    private void Update()
    {
        if (isTimerOn)
        {
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
    }

    public void Correct()
    {
        GenerateQuestion();
    }

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

    void SetupListeners()
    {
        closeButton.onClick.AddListener(() =>
        {
            quizCanvas.SetActive(false);
            answerPanel.SetActive(false);
            quizPanel.SetActive(true);
        });
    }

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

    private void LoadQuestions(string json)
    {
        questions = new List<Question>();

        List<string> errors = new List<string>();
        // TODO: check if answerIndex is between 0-3
        // TODO: check if choices has 4 elements
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
        
        questions = questions.OrderBy(x => _rnd.Next()).Take(_noQuestions).ToList();
    }
}
