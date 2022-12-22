using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Question
{
    public string utterance;
    public string[] choices;
    public int answerIndex;
}
