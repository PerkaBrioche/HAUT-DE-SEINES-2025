using UnityEngine;

public class SmokeController : MonoBehaviour
{
    [SerializeField] private GameObject _idleSmoke;
    [SerializeField] private GameObject _disapearSmoke;
    
    
    public void Diseapear()
    {
        _idleSmoke.SetActive(false);    
        _disapearSmoke.SetActive(true);
        Destroy(gameObject, 2f);
    }
}
