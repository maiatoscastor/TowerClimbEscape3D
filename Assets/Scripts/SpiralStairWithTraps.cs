using UnityEngine;
using System.Collections.Generic;

public class SpiralStairWithTraps : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject[] trapPrefabs;
    public float chanceOfTrap = 0.5f;
    public float stepHeight = 3f;
    public float radius = 10f;
    public float angleStep = 30f;
    public Vector3 startPosition;
    public Transform player;
    public float safeDistance = 50f;

    private float currentAngle = 0f;
    private float lastY = 0f;
    private List<GameObject> steps = new List<GameObject>();

    void Start()
    {
        if (blockPrefab == null)
        {
            Debug.LogError("❌ Nenhum bloco de escada atribuído!");
            return;
        }

        lastY = startPosition.y;
        GenerateStep(lastY);
    }

    void Update()
    {
        float playerY = player.position.y;

        // Gera nova escada se o jogador estiver a menos de 50 unidades da última
        if (playerY + safeDistance > lastY)
        {
            lastY += stepHeight;
            GenerateStep(lastY);
        }

        // Remove escadas muito abaixo do jogador
        for (int i = steps.Count - 1; i >= 0; i--)
        {
            if (steps[i].transform.position.y < playerY - safeDistance)
            {
                Destroy(steps[i]);
                steps.RemoveAt(i);
            }
        }
    }

    void GenerateStep(float y)
    {
        float rad = currentAngle * Mathf.Deg2Rad;
        float x = Mathf.Cos(rad) * radius;
        float z = Mathf.Sin(rad) * radius;

        Vector3 position = startPosition + new Vector3(x, y, z);
        Quaternion rotation = Quaternion.Euler(0f, -currentAngle, 0f);

        GameObject block = Instantiate(blockPrefab, position, rotation, transform);
        steps.Add(block);

        TryPlaceTrapOnBlock(block);
        currentAngle += angleStep;
    }

    void TryPlaceTrapOnBlock(GameObject block)
{
    if (trapPrefabs.Length == 0 || Random.value > chanceOfTrap)
        return;

    GameObject trap = trapPrefabs[Random.Range(0, trapPrefabs.Length)];

    Renderer trapRenderer = trap.GetComponent<Renderer>();
    Renderer blockRenderer = block.GetComponent<Renderer>();

    if (trapRenderer == null || blockRenderer == null)
        return;

    Vector3 trapSize = trapRenderer.bounds.size;
    Bounds blockBounds = blockRenderer.bounds;

    float minX = blockBounds.min.x + trapSize.x / 2f;
    float maxX = blockBounds.max.x - trapSize.x / 2f;
    float minZ = blockBounds.min.z + trapSize.z / 2f;
    float maxZ = blockBounds.max.z - trapSize.z / 2f;

    float posX = Random.Range(minX, maxX);
    float posZ = Random.Range(minZ, maxZ);
    float posY = blockBounds.max.y + 0.4f;

    Vector3 trapPosition = new Vector3(posX, posY, posZ);

    // ✅ Instancia corretamente com rotação e escala relativa
    GameObject trapInstance = Instantiate(trap, block.transform);
    trapInstance.transform.localPosition = block.transform.InverseTransformPoint(trapPosition);
    trapInstance.transform.localRotation = trap.transform.localRotation;
    trapInstance.transform.localScale = trap.transform.localScale;
}
}
