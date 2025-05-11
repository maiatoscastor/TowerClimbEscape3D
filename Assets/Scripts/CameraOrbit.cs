using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // o player
    public Vector3 offset = new Vector3(0, 2, -5);
    public float sensitivity = 5f;
    public float distance = 5f;

    private float currentX = 0f;
    private float currentY = 20f;
    public float minY = -20f;
    public float maxY = 80f;

    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 dir = new Vector3(0, 0, -distance);
        transform.position = target.position + rotation * dir + offset;

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
