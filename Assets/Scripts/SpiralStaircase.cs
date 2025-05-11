using UnityEngine;

public class SpiralStaircase : MonoBehaviour
{
    public GameObject blockPrefab;       // Prefab do bloco
    public int stepsToGenerate = 100;    // Quantos blocos criar
    public float stepHeight = 3f;        // Altura entre blocos
    public float radius = 10f;           // Distância ao centro
    public float angleStep = 30f;        // Rotação por bloco
    public Vector3 startPosition;        // Posição inicial da espiral

    void Start()
    {
        if (blockPrefab == null)
        {
            Debug.LogError("❌ Nenhum bloco atribuído!");
            return;
        }

        float currentAngle = 0f;

        for (int i = 0; i < stepsToGenerate; i++)
        {
            float rad = currentAngle * Mathf.Deg2Rad;
            float x = Mathf.Cos(rad) * radius;
            float z = Mathf.Sin(rad) * radius;
            float y = i * stepHeight;

            Vector3 position = startPosition + new Vector3(x, y, z);
            Quaternion rotation = Quaternion.Euler(0f, -currentAngle, 0f);

            Instantiate(blockPrefab, position, rotation, transform);

            currentAngle += angleStep;
        }
    }
}
