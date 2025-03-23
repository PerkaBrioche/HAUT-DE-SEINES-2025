using System;
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

    private int _smokeAmount;
    private int _smokesCleared = 0;
    
    private bool _end = false;

        public Transform GetCameraPos()
        {
            return _transformCameraPos;
        }   
    private void Start()
    {
        _smokeAmount = smokes.Count;
        _smokesTryMax = _smokesTry;
        AddSmokeToClear();
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

    private void AddSmokeToClear()
    {
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
                    print("SMOKE NOT CLEARED = " +  smoke.name);
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
            _end = true;
            ClearAll();
        }
    }
    
    private void ClearAll()
    {
        foreach (var smoke in smokes)
        {
            smoke.Diseapear();
        }
        
        CameraManager.instance.BackToDefaultPosition();
    }
}
