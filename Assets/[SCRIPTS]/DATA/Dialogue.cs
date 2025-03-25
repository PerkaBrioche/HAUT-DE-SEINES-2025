using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DIALOGUE/Dialogue")]
public class Dialogue : ScriptableObject
{
    public  List<DialogueLine> dialogueLines;
    [Serializable]
    public struct DialogueLine
    {
        public string text;
        [Header("SMOKE TO CLEAR : EX : 0-20")]
        public List<int> _smokesToClearEmplacement;
    };
}
