using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class characterController : MonoBehaviour
{

    [Header("CHARACTER INFO")] [SerializeField]
    private float _movingTime;
    [Foldout("OTHERS")]
    [SerializeField] private bounce3D _bounce;
    [Foldout("OTHERS")]
    [SerializeField] private Transform _transformDialogueSpawnPoint;
    [Foldout("OTHERS")]
    [SerializeField] private textController _textController;
    [Foldout("OTHERS")]
    [SerializeField] private GameObject _characterMesh;
    [Foldout("OTHERS")]
    [SerializeField] private Animator _animatorCharacter;
    [Foldout("OTHERS")]
    [SerializeField] private ParticleSystem _particleSystem;
    [Foldout("OTHERS")]
    private Dialogue _currentDialogue;

    // [SerializeField] private Transform _destinationTest;
    //  [Button]
    //  public void TestDestination()
    //  {
    //      SetDestination(_destinationTest);
    //  }
    
    
    public void SetDestination(Transform destination)
    {
        StartCoroutine(MoveToDestination(destination));
    }
    
    private IEnumerator MoveToDestination(Transform destination)
    {
        _animatorCharacter.SetBool("isWalking", true);
        float alpha = 0;
        Vector3 originalPos = transform.position;
        Vector3 destinationPos = new Vector3(destination.position.x, originalPos.y, destination.position.z);
        while (alpha < 1)
        {
            transform.position = Vector3.Lerp(originalPos, destinationPos, alpha);
            alpha += Time.deltaTime / _movingTime;
            yield return null;
        }
        transform.position = destinationPos;
        DestinationEnd();
        yield return null;
    }

    private void DestinationEnd()
    {
        _animatorCharacter.SetBool("isWalking", false);
        dialoguemanager.instance.DestinationChecked();
    }
    
    
    public void CallDialogue(Dialogue dialogue)
    {
        _textController.CallDialogue(dialogue);
    }
    public bounce3D GetBounce()
    {
        return _bounce;
    }

    private void OnValidate()
    {
        if (_movingTime < 1)
        {
            _movingTime = 1;
        }
    }

    public void DiseapearCharacter()
    {
        StartCoroutine(Diseapear());
    }

    private IEnumerator Diseapear()
    {
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        _characterMesh.SetActive(false);
        yield return new WaitForSeconds(2);
        _characterMesh.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void AppearingCharacter()
    {
        StartCoroutine(Appear());
    }
    
    private IEnumerator Appear()
    {
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        _characterMesh.SetActive(true);
    }

    public void TeleportCharacter(Transform destination)
    {
        transform.position = new Vector3(destination.position.x,  transform.position.y, destination.position.z);
    }
}
