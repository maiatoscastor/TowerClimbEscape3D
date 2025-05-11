using UnityEngine;
using System.Collections;

public class LaserTrap : MonoBehaviour
{
    public float warningTime = 0.5f;   // Tempo do aviso visual (sem dano)
    public float activeTime = 2f;      // Tempo ativo (com dano)
    public float inactiveTime = 2f;    // Tempo desligado após o dano

    private Renderer rend;
    private Collider col;
    private PlayerLives playerLives; // Agora é privado, e será atribuído por código

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();

        // Atribui automaticamente o PlayerLives da cena
        playerLives = FindFirstObjectByType<PlayerLives>();

        StartCoroutine(LaserCycle());
    }

    IEnumerator LaserCycle()
    {
        while (true)
        {
            // ⚠️ 1. Aviso visual (sem dano)
            rend.enabled = true;
            col.enabled = false;
            yield return new WaitForSeconds(warningTime);

            // ⏸️ 2. Desligado após aviso
            rend.enabled = false;
            yield return new WaitForSeconds(warningTime);

            // 💀 3. Ativo com dano
            rend.enabled = true;
            col.enabled = true;
            yield return new WaitForSeconds(activeTime);

            // 💤 4. Desligado após fase com dano
            rend.enabled = false;
            col.enabled = false;
            yield return new WaitForSeconds(inactiveTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerLives != null)
        {
            Debug.Log("☠️ Jogador atingido pelo laser!");
            playerLives.TakeDamage();

            if (playerLives.totalLives <= 0)
            {
                playerLives.gameOverManager.GameOver();
            }
        }
    }
}
