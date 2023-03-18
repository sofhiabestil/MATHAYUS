using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PDifficultQuestionsData", menuName = "PDifficultQuestionsData", order = 1)]
public class PDifficultDataScriptable : ScriptableObject
{
    public List<PDifficultQuestionData> Pdifficultquestions;
}
