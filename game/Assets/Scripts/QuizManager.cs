using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public List<Question> questions;
    public GameObject[] options;
    public int currentQuestion = 0;
    public TextMeshProUGUI questionText;

    private void Start()
    {
        LoadQuestions();
        GenerateQuestion();
    }

    // FIXME: this is just a hack for now
    private void End()
    {
        Debug.Log("No more questions!");
    }

    public void Correct()
    {
        currentQuestion++;
        if (questions.Count <= currentQuestion)
        {
            End();
        }
        else
        {
            GenerateQuestion();
        }
    }

    void SetAnswers()
    {
        Question question = questions[currentQuestion];
        for (int i = 0; i < options.Length; i++)
        {
            var option = options[i];
            option.GetComponent<AnswerManager>().isCorrect = false;
            option.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.choices[i];

            if (question.answerIndex == i)
            {
                option.GetComponent<AnswerManager>().isCorrect = true;
            }
        }
    }

    private void GenerateQuestion()
    {
        questionText.text = questions[currentQuestion].utterance;

        SetAnswers();
    }

    private void LoadQuestions()
    {
        questions = new List<Question>();

        using StreamReader r = new StreamReader("questions.json");
        string json = r.ReadToEnd();
        questions = JsonConvert.DeserializeObject<List<Question>>(json);
    }
}
