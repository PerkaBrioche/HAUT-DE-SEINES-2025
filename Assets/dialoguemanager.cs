using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        public bool _isDestination;
        public Transform _destination;
        public UnityEvent _action;
    }

    public void DestinationChecked()
    {
        NextDialogue();
    }

    private void Start()
    {
        NextDialogue();
    }

    public void NextDialogue()
    {
        if(_dialogueSequences[_currentDialogueIndex]._action != null)
        {
            _dialogueSequences[_currentDialogueIndex]._action.Invoke();
        }
        if (_currentDialogueIndex >= _dialogueSequences.Count) {return;} // END GAME
        
        characterController character = _dialogueSequences[_currentDialogueIndex].Character;

        if(_dialogueSequences[_currentDialogueIndex]._isDestination)
        {
            character.SetDestination(_dialogueSequences[_currentDialogueIndex]._destination);
        }
        else
        {
            Dialogue dialogue = _dialogueSequences[_currentDialogueIndex].Dialogue;
            character.CallDialogue(dialogue);
        }
        
        _currentDialogueIndex++;

    }
}
