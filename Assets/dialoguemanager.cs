using System;
using System.Collections.Generic;
using UnityEngine;

public class dialoguemanager : MonoBehaviour
{
    public static dialoguemanager instance; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public List<_DialogueSequence> _dialogueSequences;
    private int _currentDialogueIndex = 0;
    [Serializable]
    public struct _DialogueSequence
    {
        public characterController Character;
        public Dialogue Dialogue;
    }

    private void Start()
    {
        NextDialogue();
    }

    public void NextDialogue()
    {
        if (_currentDialogueIndex >= _dialogueSequences.Count)
        {
            return;
        }
        characterController character = _dialogueSequences[_currentDialogueIndex].Character;
        Dialogue dialogue = _dialogueSequences[_currentDialogueIndex].Dialogue;
        
        _currentDialogueIndex++;
        
        character.CallDialogue(dialogue);
    }
}
