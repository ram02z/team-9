using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TMPro;
using UnityEngine;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

public class QuizManager : MonoBehaviour
{
    public List<Question> questions;
    public GameObject[] options;
    public Question currentQuestion;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerText;
    public GameObject quizPanel;
    public GameObject answerPanel;

    private readonly int _noQuestions = 3;

    private void Start()
    {
        using StreamReader r = new StreamReader("questions.json");
        string json = r.ReadToEnd();
        LoadQuestions(json);
        GenerateQuestion();
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
        
        var rnd = new System.Random();
        questions = questions.OrderBy(x => rnd.Next()).Take(_noQuestions).ToList();
    }
}
