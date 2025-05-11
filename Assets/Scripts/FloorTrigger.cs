using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public Transform blockToRotate1;
    public Vector3 rotationAmount1;

    public Transform blockToRotate2;
    public Vector3 rotationAmount2;

    public float rotationSpeed = 90f;
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(RotateBlock(blockToRotate1, rotationAmount1));
            StartCoroutine(RotateBlock(blockToRotate2, rotationAmount2));
        }
    }

    System.Collections.IEnumerator RotateBlock(Transform block, Vector3 rotationAmount)
    {
        Quaternion startRotation = block.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(rotationAmount);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed / 90f;
            block.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
    }
}
