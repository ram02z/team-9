using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

public class QuizManager : MonoBehaviour
{
    public List<int> codes;
    public List<Question> questions;
    public GameObject[] options;
    public GameObject[] interactables;
    public TextMeshProUGUI questionText;
    public GameObject quizCanvas;
    public GameObject cooldownPanel;
    public GameObject codePanel;
    public TextMeshProUGUI codeText;
    public TextMeshProUGUI cooldownText;
    public GameObject quizPanel;
    public GameObject answerPanel;
    public Button closeButton;
    private System.Random _rnd;


    /// <summary>
    /// Initialise the quiz manager
    /// </summary>
    private void Start()
    {
        _rnd = new System.Random();

        // Create N 4 digit codes
        codes = Enumerable.Range(1000, 9000).OrderBy(_ => _rnd.Next()).Take(interactables.Length).ToList();

        // Load questions from JSON file
        using StreamReader r = new StreamReader("questions.json");
        string json = r.ReadToEnd();

        // Initialise quiz
        LoadQuestions(json);
        AttachQuestionsToInteractables();
        SetupListeners();
    }

    private void AttachQuestionsToInteractables()
    {
        for (int i = 0; i < interactables.Length; i++)
        {
            var interactable = interactables[i];
            int randomIdx = _rnd.Next(questions.Count);
            interactable.GetComponent<Question_Interactor>().Question = questions[randomIdx];
            questions.RemoveAt(randomIdx);
            interactable.GetComponent<Question_Interactor>().Code = codes[0];
            codes.RemoveAt(0);
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
            // TODO: unlock position and camera
        });
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
        }
    }
}
