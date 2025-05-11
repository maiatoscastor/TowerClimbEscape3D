using UnityEngine;
using System.Collections.Generic;

public class DynamicStairsGenerator : MonoBehaviour
{
    public GameObject stairPrefab;             // Prefab da escada
    public Transform player;                   // Referência ao jogador
    public float heightOffset = 10f;           // Espaçamento vertical entre escadas
    public float safeDistance = 50f;           // Distância acima/abaixo do jogador para gerar/remover
    public Vector3 startPosition;              // Posição inicial das escadas
    public Vector3 startRotationEuler;         // Rotação inicial das escadas

    private float nextSpawnHeight;
    private List<GameObject> activeStairs = new List<GameObject>();

    void Start()
    {
        nextSpawnHeight = startPosition.y;

        // Cria a primeira escada
        SpawnStair(nextSpawnHeight);
        nextSpawnHeight += heightOffset;
    }

    void Update()
    {
        float playerY = player.position.y;

        // Gera escadas acima se o jogador estiver próximo do topo
        if (playerY + safeDistance >= nextSpawnHeight)
        {
            SpawnStair(nextSpawnHeight);
            nextSpawnHeight += heightOffset;
        }

        // Remove escadas que ficaram muito abaixo do jogador
        for (int i = activeStairs.Count - 1; i >= 0; i--)
        {
            if (activeStairs[i].transform.position.y < playerY - safeDistance)
            {
                Destroy(activeStairs[i]);
                activeStairs.RemoveAt(i);
            }
        }
    }

    void SpawnStair(float height)
    {
        Vector3 spawnPosition = new Vector3(startPosition.x, height, startPosition.z);
        Quaternion rotation = Quaternion.Euler(startRotationEuler);

        GameObject newStair = Instantiate(stairPrefab, spawnPosition, rotation, transform);
        activeStairs.Add(newStair);
    }
}
