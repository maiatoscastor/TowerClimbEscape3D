using UnityEngine;

public class SpikeTrapDamage : MonoBehaviour
{
    private PlayerLives playerLives;

    void Start()
    {
        playerLives = FindFirstObjectByType<PlayerLives>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerLives != null)
        {
            Debug.Log("☠️ Espinhos mataram o jogador!");
            playerLives.TakeDamage();

            if (playerLives.totalLives <= 0)
            {
                playerLives.gameOverManager.GameOver();
            }
        }
    }
}
