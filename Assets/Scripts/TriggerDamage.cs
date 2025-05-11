using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public GameOverManager gameOverManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jogador caiu aonde nao devia!");
            gameOverManager.GameOver();
        }
    }
}
