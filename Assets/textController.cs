using System;
using Febucci.UI;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class textController : MonoBehaviour
{
    [Header("DIALOGUE INFO")]
    
    public Dialogue dialogue;
    
    [Space(20)]
    
    [Foldout("OTHERS")]
    [SerializeField] private GameObject dialogueBox;
    [Foldout("OTHERS")]
    [SerializeField] private TextMeshPro dialogueText;
    [Foldout("OTHERS")]
    [SerializeField] private TypewriterByCharacter typewriter;
    
    private bool _canSkip;

    
    private int _currentLine;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndLine();
        }
    }

    public void EndLine()
    {
        if (_canSkip)
        {
            _canSkip = false;
            _currentLine++;
            if (_currentLine < dialogue.dialogueLines.Count)
            {
                NewLine();
            }
            else
            {
                EndDialogue();
            }
        }
        else
        {
            _canSkip = true;
            typewriter.SkipTypewriter();
        }
    }
    
    public void EndDialogue()
    {
    }
    
    public void NewLine()
    {
        typewriter.ShowText(dialogue.dialogueLines[_currentLine]);
    }
    
    public void SetSkip(bool value)
    {
        _canSkip = value;
    }
}
