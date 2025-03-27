using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UmbrellaGame : MonoBehaviour
{
    [SerializeField] private UnityEvent OnFinished;
    [SerializeField] private float speed;
    [SerializeField] private Transform umbrella;
    [SerializeField] private TextMeshProUGUI tmp;
    public string text;
    float startX = 0f;

    private void Start()
    {
        startX = umbrella.position.x;
        tmp.text = text;
        tmp.ForceMeshUpdate();
        textWidth = tmp.GetRenderedValues().x;
    }

    private bool hasFinished = false;
    private float textWidth;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !hasFinished)
        {
            umbrella.position += Vector3.right * speed * Time.deltaTime;

            Vector3 pos = umbrella.position;
            pos.x = Mathf.Clamp(pos.x, startX, startX + textWidth);
            umbrella.position = pos;

            if (umbrella.position.x >= startX + textWidth)
            {
                OnFinished.Invoke();
                hasFinished = true;
            }
        }
    }
}
