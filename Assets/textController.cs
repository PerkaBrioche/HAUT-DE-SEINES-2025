using System;
using System.Collections;
using Febucci.UI;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class textController : MonoBehaviour
{
    [Header("DIALOGUE INFO")]
    
    private Dialogue dialogue;
    
    [Space(20)]
    
    [Foldout("OTHERS")]
    [SerializeField] private GameObject dialogueBox;
    [Foldout("OTHERS")]
    [SerializeField] private TextMeshPro dialogueText;
    [Foldout("OTHERS")]
    [SerializeField] private TypewriterByCharacter typewriter;
    [Foldout("OTHERS")]
    [SerializeField] private GameObject _skipIndicator;
    private SmokeClearer _smokeClearer;

    [Foldout("OTHERS")]
    [SerializeField] private bounce3D _bounce3D;
    private bool _canSkip;
    private bool _inSmokePhase = false;
    public void SetBounce3D(bounce3D bounce)
    {
        _bounce3D = bounce;
    }
    
    private int _currentLine;

    private bool _dialogueStarted = false;
    private bool _dialogueEnded = false;
    
    private bool _unlocked = false;
    private bounce3D _bounce;
    private bool _canBounce = true;

    public void CallDialogue(Dialogue dialogue)
    {
        _unlocked = true;
        this.dialogue = dialogue;
        dialogueBox.SetActive(true);
        NewLine();
    }

    private void ResetDialogue()
    {
        _currentLine = 0;
        _dialogueStarted = false;
        _dialogueEnded = false;
        _unlocked = false;
    }

    [Button ("Unlock")]
    public void Unlock()
    {
        SetUnlocked(true);
    }   
    public void SetUnlocked(bool value)
    {
        _unlocked = value;
    }

    private void Awake()
    {
        _smokeClearer = gameObject.GetComponent<SmokeClearer>();
    }

    private void Start()
    {
        SetMeshskip(false);
        _bounce = dialogueBox.GetComponent<bounce3D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _unlocked && !_inSmokePhase)
        {
            EndLine();
        }
        
        if(_unlocked && !_dialogueEnded)
        {
            dialogueBox.SetActive(true);
        }
    }

    public void EndLine()
    {
        if(_dialogueEnded) return;
        if (_canSkip)
        {
            SetSkip(false);
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
            SetSkip(true);
            typewriter.SkipTypewriter();
        }
    }
    
    public void EndDialogue()
    {
        _dialogueEnded = true;
        dialogueText.text = "";
        ResetDialogue();
        dialoguemanager.instance.NextDialogue();
        _bounce.BounceDisapear();
    }

    public void SmokeCleared()
    {
        _inSmokePhase = false;
        _smokeClearer.enabled = false;
    }
    
    public void NewLine()
    {
        _bounce3D.StopAllCoroutines();
        if (!dialogueBox.activeInHierarchy)
        {
            dialogueBox.SetActive(true); 
        }
        typewriter.ShowText(dialogue.dialogueLines[_currentLine].text);
        if (dialogue.dialogueLines[_currentLine]._smokesToClearEmplacement.Count > 0)
        {
            _inSmokePhase = true;
            _smokeClearer.enabled = true;
            _smokeClearer.StartSmokePhase(dialogue.dialogueLines[_currentLine]._smokesToClearEmplacement);
        }
        _currentLine++;
        _bounce.CustomBounce(1.1f, 0.15f, 1.1f);
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

    public void BounceCharacter()
    {
        if (!_canBounce)
        {
            StartCoroutine(WaitForEndOfAnimation()); return;
        }
        _canBounce = false;
        
        float randomForce = UnityEngine.Random.Range(1.1f, 1.3f);
        _bounce3D.CustomBounce(randomForce, 0.15f, 2f);
    }

    private IEnumerator WaitForEndOfAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        _canBounce = true;
    }
    
}
