using UnityEngine;
using UnityEngine.Events;

public class UmbrellaGame : MonoBehaviour
{
    [SerializeField] private UnityEvent OnFinished;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 constrain;
    [SerializeField] private Transform umbrella;
    float startX = 0f;

    private void OnDrawGizmos()
    {
        if(umbrella != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(umbrella.position - Vector3.right * constrain.x, 0.5f);
            Gizmos.DrawSphere(umbrella.position + Vector3.right * constrain.y, 0.5f);
        }
    }

    private void Start()
    {
        startX = umbrella.position.x;
    }

    private bool hasFinished = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            umbrella.position += Vector3.right * speed * Time.deltaTime;

            Vector3 pos = umbrella.position;
            pos.x = Mathf.Clamp(pos.x, startX - constrain.x, startX + constrain.y);
            umbrella.position = pos;

            if (umbrella.position.x >= constrain.y && !hasFinished)
            {
                OnFinished.Invoke();
                hasFinished = true;
            }
        }
    }
}
