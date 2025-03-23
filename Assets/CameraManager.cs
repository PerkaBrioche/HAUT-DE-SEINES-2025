using System;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    public static CameraManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        cam = Camera.main;
        camTransform = cam.transform.root.transform;   

        SaveDefaultPosition();
    }
    [Header("Camera Parameters")]
    [SerializeField] private float _translationTime = 1f;
    
    private Camera cam;
    private Transform camTransform; 
    private Vector3 orginalPosition;
    private Quaternion orginalRotation;


    public void SwitchProjection()
    {
        if (cam.orthographic)
        {
            cam.orthographic = false;
        }
        else
        {
            cam.orthographic = true;
        }
    }       

    public void MoveCamera(Transform target)
    {
        Vector3 positionTarget = target.position;
        Quaternion rotationTarget = target.rotation;
        StartCoroutine(TranslationCameraPosition(positionTarget, rotationTarget));
    }
    
    public void BackToDefaultPosition()
    {
        StartCoroutine(BackToDefaultPositon());
    }

    private IEnumerator TranslationCameraPosition( Vector3 positionTarget, Quaternion rotationTarget)
    {
        SwitchProjection();
        float alpha = 0f;
        
        while (alpha < 1)
        {
            alpha += Time.deltaTime / _translationTime;
            camTransform.transform.position = Vector3.Lerp(orginalPosition, positionTarget, alpha);
            camTransform.transform.rotation = Quaternion.Lerp(orginalRotation, rotationTarget, alpha);
            yield return null;
        }
        camTransform.transform.position = positionTarget;
        camTransform.transform.rotation = rotationTarget;
        yield return null;
    }   
    
    private IEnumerator BackToDefaultPositon()
    {
        float alpha = 0f;
        while (alpha < 1)
        {
            alpha += Time.deltaTime / _translationTime;
            camTransform.transform.position = Vector3.Lerp(camTransform.transform.position, orginalPosition, alpha);
            camTransform.transform.rotation = Quaternion.Lerp(camTransform.transform.rotation, orginalRotation, alpha);
            yield return null;
        }
        SwitchProjection();
        yield return null;
    }
    
    public void SaveDefaultPosition()
    {
        orginalPosition = cam.transform.position;
        orginalRotation = cam.transform.rotation;
    }
    
}
