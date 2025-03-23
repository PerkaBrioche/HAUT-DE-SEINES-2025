using UnityEngine;

public class characterController : MonoBehaviour
{
    [SerializeField] private bounce3D _bounce;
    [SerializeField] private Transform _transformDialogueSpawnPoint;
    
    public bounce3D GetBounce()
    {
        return _bounce;
    }
}
