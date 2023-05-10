using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultQuestionsData", menuName = "DifficultQuestionsData", order = 1)]
public class DifficultDataScriptable : ScriptableObject
{
    public List<DifficultQuestionData> difficultquestions;
}
