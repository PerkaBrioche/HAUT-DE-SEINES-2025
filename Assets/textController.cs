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
    [Foldout("OTHERS")]
    [SerializeField] private GameObject _skipIndicator;
    
    private bool _canSkip;

    
    private int _currentLine;

    private bool _dialogueStarted = false;
    private bool _dialogueEnded = false;


    private void Start()
    {
        SetMeshskip(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndLine();
        }
    }

    public void EndLine()
    {
        if(_dialogueEnded) return;
        if (!_dialogueStarted)
        {
            _dialogueStarted = true;
                            NewLine();
                            return;
        }
        if (_canSkip)
        {
            SetSkip(false);
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
            print("Skipping");
            SetSkip(true);
            typewriter.SkipTypewriter();
        }
    }
    
    public void EndDialogue()
    {
        _dialogueEnded = true;  
        dialogueText.text = "";
    }
    
    public void NewLine()
    {
        typewriter.ShowText(dialogue.dialogueLines[_currentLine]);
    }
    
    public void SetSkip(bool value)
    {
        _canSkip = value;
        SetMeshskip(value);
    }
    
    private void SetMeshskip(bool value)
    {
        _skipIndicator.SetActive(value);
    }
}
