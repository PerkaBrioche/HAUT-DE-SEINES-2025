using System;
using System.Collections;
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

    [Foldout("OTHERS")]
    [SerializeField] private bounce3D _bounce3D;
    private bool _canSkip;
    public void SetBounce3D(bounce3D bounce)
    {
        _bounce3D = bounce;
    }
    
    private int _currentLine;

    private bool _dialogueStarted = false;
    private bool _dialogueEnded = false;
    
    [SerializeField] private bool _unlocked = false;
    
    private bounce3D _bounce;
    
    private bool _canBounce = true;


    [Button ("Unlock")]
    public void Unlock()
    {
        SetUnlocked(true);
    }   
    public void SetUnlocked(bool value)
    {
        _unlocked = value;
        if (_unlocked)
        {
            NewLine();
        }
    }
    
    
    private void Start()
    {
        SetMeshskip(false);
        _bounce = dialogueBox.GetComponent<bounce3D>();
    }

    private void OnEnable()
    {
        NewLine();
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
        _bounce.BounceDisapear();
    }
    
    public void NewLine()
    {
        typewriter.ShowText(dialogue.dialogueLines[_currentLine]);
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
