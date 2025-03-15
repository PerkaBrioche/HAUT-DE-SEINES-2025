using System.Collections;
using UnityEngine;

public class bounce3D : MonoBehaviour
{
    public bool bounceOnEnable = false;
    public float bounceForce = 1.2f;
    public float bounceDuration = 1.2f;
    public float bounceDecreaseSpeed = 3f;

    private Vector3 originalScale;
    private Vector3 originalPosition;

    private bool _isHilighted = false;
    private void Awake()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    private void OnEnable()
    {
        if (bounceOnEnable)
        {
            StartBounce();
        }
    }
    
    public void StartBounce()
    {
        StopAllCoroutines();
        StartCoroutine(BounceCoroutine(false));
    }
    
    public void HiglightBounce()
    {
        if(_isHilighted) {return;}
        
        _isHilighted = true;
        StopAllCoroutines();
        StartCoroutine(BounceCoroutine(true));
    }
    
    public void EndHighlight()
    {
        _isHilighted = false;
        StopAllCoroutines();
        StartCoroutine(BounceCoroutine(false, bounceDuration));
    }
    
    private IEnumerator BounceCoroutine(bool locks, float timers = 0)
    {
        float timer = timers;
        Vector3 targetScale = originalScale * bounceForce;
        Vector3 targetPosition = originalPosition;

        while (timer < bounceDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / bounceDuration);
            // SmoothStep pour un easing plus fluide
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, smoothT);
            transform.position = Vector3.Lerp(originalPosition, targetPosition, smoothT);
            yield return null;
        }

        if (locks)
        {
            yield break;
        }
        while (timer > 0f)
        {
            timer -= Time.deltaTime * bounceDecreaseSpeed;
            float t = Mathf.Clamp01(timer / bounceDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, smoothT);
            transform.position = Vector3.Lerp(originalPosition, targetPosition, smoothT);
            yield return null;
        }

        transform.localScale = originalScale;
        transform.position = originalPosition;
    }

    public void ResetBounce()
    {
        StopAllCoroutines();
        transform.localScale = originalScale;
        transform.position = originalPosition;
    }
}
