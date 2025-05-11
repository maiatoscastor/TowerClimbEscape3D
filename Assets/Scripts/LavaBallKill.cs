using UnityEngine;

public class LavaBallKill : MonoBehaviour
{
    private PlayerLives playerLives;

    void Start()
    {
        // Procura automaticamente o PlayerLives na cena
        playerLives = FindFirstObjectByType<PlayerLives>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerLives != null)
        {
            Debug.Log("☠️ Bola de lava queimou o jogador!");
            playerLives.TakeDamage();

            if (playerLives.totalLives <= 0)
            {
                playerLives.gameOverManager.GameOver();
            }
        }
    }
}