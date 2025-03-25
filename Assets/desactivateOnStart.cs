using UnityEngine;

public class desactivateOnStart : MonoBehaviour
{

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
