using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class SmokeClearer : MonoBehaviour
{

    [Header("SOMKE PARAMETER")]
    [SerializeField] private int _smokesTry = 0; private int _smokesTryMax;
    
    [Foldout("OTHERS")]
    [SerializeField] private Image _smokeJauge;
    [Foldout("OTHERS")]
    [SerializeField] private List<SmokeController> smokes;
    [Foldout("OTHERS")]
    [SerializeField] private Transform _transformCameraPos;
    [Foldout("OTHERS")]
    [SerializeField] private List<SmokeController> smokesToClear;
    [Foldout("OTHERS")]
    [SerializeField] private textController textController;

    [Foldout("OTHERS")] [SerializeField] private GameObject _SmokesAndCanvas;
    [Foldout("OTHERS")] [SerializeField] private Transform _allSmokesTransform;

    private int _smokeAmount;
    private int _smokesCleared = 0;
    
    private bool _end = false;

    public Transform GetCameraPos()
    {
        return _transformCameraPos;
    }
        

    public void StartSmokePhase(List<int> smokeIndex)
    {
        _SmokesAndCanvas.SetActive(true);
        _smokeAmount = smokes.Count;
        _smokesTryMax = _smokesTry;
        AddSmokeToClear(smokeIndex);
        
        CameraManager.instance.MoveCamera(_transformCameraPos);
    }



    public void ClearSmoke()
    {
        if(_end) return;
        _smokesTry--;
        _smokesCleared++;
        float fillAmount = (float)_smokesTry / _smokesTryMax;
        _smokeJauge.fillAmount =fillAmount;
        
        CheckSmoke();
    }
    
    private void CheckSmoke()
    {
        CheckSmokeToClear();
        
        if(_end){return;}
        if(_smokesTry <= 0)
        {
            _end = true;
            return;
        }
    }

    private void AddSmokeToClear(List<int> smokeIndex)
    {
        foreach (var index in smokeIndex)
        {
            _allSmokesTransform.GetChild(index).gameObject.SetActive(false);
        }
        foreach (var smoke in smokes)
        {
            if(!smoke.gameObject.activeInHierarchy)
            {
                smokesToClear.Add(smoke);
                smoke.gameObject.SetActive(true);
            }
        }
        
    }
    private void CheckSmokeToClear()
    {
        bool win = true;
        foreach (var smoke in smokesToClear)
        { 
            if(smoke != null)
            {
                if (!smoke.cleared)
                {
                    win = false;
                    break;
                }
                else
                {
                    print("SMOKE CLEARED");
                }
            }
            else
            {
                print("SMOKE NULL");
            }
        }
        
        if (win)
        {
            StopAllCoroutines();
            _end = true;
            ClearAll();
        }
        else
        {
            StopCoroutine("SecondCheck");
            StartCoroutine("SecondCheck");
        }
    }

    private IEnumerator SecondCheck()
    {
        yield return new WaitForSecondsRealtime(1f);
        CheckSmokeToClear();
    }
    
    private void ClearAll()
    {
        foreach (var smoke in smokes)
        {
            smoke.Diseapear();
        }
        textController.SmokeCleared();
        
        CameraManager.instance.BackToDefaultPosition();
    }
}
