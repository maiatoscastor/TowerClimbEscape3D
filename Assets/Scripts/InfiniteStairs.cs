using UnityEngine;
using System.Collections.Generic;

public class InfiniteStairs : MonoBehaviour
{
    public GameObject[] stairPrefabs;       // Prefabs variados
    public Transform player;                // Referência ao jogador
    public float heightOffset = 10f;        // Espaço entre escadas
    public float safeDistance = 50f;        // Distância limite antes de destruir escadas
    public Vector3 startPosition;           // Posição inicial definida no Inspector
    public Vector3 startRotationEuler;      // Rotação inicial das escadas

    private List<GameObject> stairs = new List<GameObject>();
    private float lastY;
    private int lastPrefabIndex = -1;

    void Start()
    {
        lastY = startPosition.y;
        SpawnStair(lastY);
    }

    void Update()
    {
        float playerY = player.position.y;

        // Adiciona nova escada acima se o jogador estiver perto da última
        if (playerY + safeDistance > lastY)
        {
            lastY += heightOffset;
            SpawnStair(lastY);
        }

        // Remove escadas muito abaixo do jogador
        for (int i = stairs.Count - 1; i >= 0; i--)
        {
            if (stairs[i].transform.position.y < playerY - safeDistance)
            {
                Destroy(stairs[i]);
                stairs.RemoveAt(i);
            }
        }
    }

    void SpawnStair(float yPosition)
    {
        if (stairPrefabs.Length == 0)
        {
            Debug.LogWarning("❌ Nenhum prefab de escada definido!");
            return;
        }

        // Escolher prefab aleatório diferente do anterior
        int prefabIndex;
        do
        {
            prefabIndex = Random.Range(0, stairPrefabs.Length);
        } while (prefabIndex == lastPrefabIndex && stairPrefabs.Length > 1);

        lastPrefabIndex = prefabIndex;
        GameObject prefab = stairPrefabs[prefabIndex];

        Vector3 position = new Vector3(startPosition.x, yPosition, startPosition.z);
        Quaternion rotation = Quaternion.Euler(startRotationEuler);

        GameObject stair = Instantiate(prefab, position, rotation, transform);
        stairs.Add(stair);
    }
}
