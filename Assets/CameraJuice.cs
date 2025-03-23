using UnityEngine;

public class CameraJuice : MonoBehaviour
{
    public float rotationAmount = 10f;  
    public float rotationSmoothTime = 0.1f;

    public float positionAmount = 0.5f;
    public float positionSmoothTime = 0.1f;
    public bool usePositionOffset = false;

    private Vector3 rotationVelocity;
    private Vector3 positionVelocity;

    private Vector3 initialLocalEulerAngles;
    private Vector3 initialLocalPosition;

    void Start()
    {
        initialLocalEulerAngles = transform.localEulerAngles;
    }

    void Update()
    {
        Vector2 viewportMousePos = Camera.main.ScreenToViewportPoint(-Input.mousePosition);
        Vector2 offset = viewportMousePos - new Vector2(0.5f, 0.5f);

        Vector3 targetRotationOffset = new Vector3(offset.y * rotationAmount, -offset.x * rotationAmount, 0);
        Vector3 desiredRotation = initialLocalEulerAngles + targetRotationOffset;

        float smoothX = Mathf.SmoothDampAngle( desiredRotation.x, transform.localEulerAngles.x, ref rotationVelocity.x, rotationSmoothTime);
        float smoothY = Mathf.SmoothDampAngle(desiredRotation.y, transform.localEulerAngles.y,  ref rotationVelocity.y, rotationSmoothTime);
        float smoothZ = Mathf.SmoothDampAngle( desiredRotation.z,  transform.localEulerAngles.z,ref rotationVelocity.z, rotationSmoothTime);
        transform.localEulerAngles = new Vector3(smoothX, smoothY, smoothZ);
    }
}
