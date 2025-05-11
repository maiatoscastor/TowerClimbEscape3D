using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaRise : MonoBehaviour
{
    public float speed = 0.4f;
    public GameOverManager gameOverManager;

    public bool isActive = false; // lava só sobe se estiver ativa

    void Update()
    {
        if (!isActive) return; // Ignora se não tiver sido ativada

        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🌋 Jogador tocou na lava!");
            gameOverManager.GameOver();
        }
    }
}