using UnityEngine;

public class AutoGetCamera : MonoBehaviour
{
    void Start()
    {
        if(TryGetComponent<Canvas>(out Canvas canvas))
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
