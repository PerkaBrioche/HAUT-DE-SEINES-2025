using System.Collections;
using UnityEngine;

public class bounce3D : MonoBehaviour
{
    public bool bounceOnEnable = false;
    public float bounceForce = 1.2f;
    public float bounceDuration = 1.2f;
    public float bounceDecreaseSpeed = 3f;

    private Vector3 originalScale;

    private bool _isHilighted = false;
    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void StopAllcoroutines()
    {
        StopAllCoroutines();
    }
    
    public void CustomBounce(float force, float duration, float decreaseSpeed)
    {
        StartCoroutine(CustomBounceRoutine(force, duration, decreaseSpeed));
    }

    private IEnumerator CustomBounceRoutine(float force, float duration, float decreaseSpeed)
    {
        
        float timer = 0;
        Vector3 targetScale = originalScale * force;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, smoothT);
            yield return null;
        }

        while (timer > 0f)
        {
            timer -= Time.deltaTime * decreaseSpeed;
            float t = Mathf.Clamp01(timer / duration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, smoothT);
            yield return null;
        }

        transform.localScale = originalScale;
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

        while (timer < bounceDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / bounceDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, smoothT);
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
            yield return null;
        }

        transform.localScale = originalScale;
    }
    private IEnumerator DispearRoutine()
    {
        float alpha = 0;
        Vector3 targetScale = new Vector3(0, 0, 0);

        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            float t = Mathf.Clamp01(alpha / bounceDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(originalScale,targetScale , smoothT);
            yield return null;
        }
        gameObject.SetActive(false);
        transform.localScale = originalScale;
    }
    public void BounceDisapear()
    {
        StopAllCoroutines();
        StartCoroutine(DispearRoutine());
    }

    public void ResetBounce()
    {
        StopAllCoroutines();
        transform.localScale = originalScale;
    }
}
