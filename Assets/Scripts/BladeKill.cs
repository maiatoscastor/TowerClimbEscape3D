using UnityEngine;

public class BladeKill : MonoBehaviour
{
    private PlayerLives playerLives;

    void Start()
    {
        // Procura automaticamente o PlayerLives na cena
       playerLives = FindFirstObjectByType<PlayerLives>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerLives != null)
            {
                Debug.Log("☠️ Lámina Giratória matou o jogador!");
                playerLives.TakeDamage();

                if (playerLives.totalLives <= 0)
                {
                    playerLives.gameOverManager.GameOver();
                }
            }
        }
    }
}
