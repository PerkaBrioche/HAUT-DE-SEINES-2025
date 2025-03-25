using System;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
    [SerializeField] private GameObject _idleSmoke;
    [SerializeField] private GameObject _disapearSmoke;
    
    private bool _isDisapearing = false;
    
    public bool cleared = false;

    private SmokeClearer _smokeClearer;
    private void Awake()
    {
        _smokeClearer = transform.parent.parent.parent.GetComponent<SmokeClearer>();

    }

    public void Diseapear()
    {
        if (_isDisapearing) return;
        _isDisapearing = true;
        cleared = true;
        _idleSmoke.SetActive(false);    
        _disapearSmoke.SetActive(true);
        _smokeClearer.ClearSmoke();
        Destroy(gameObject, 2f);
    }
    
    
    
    
}
