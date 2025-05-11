using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeTrap3 : MonoBehaviour
{
    public float height = 1.5f;
    public float speed = 2f;
    public float delay = 1f;
    public float startOffset = 0f;

    private Vector3 startPos;
    private Vector3 topPos;
    private bool goingUp = true;
    private float timer = 0f;
    private bool started = false;

    private PlayerLives playerLives;

    void Start()
    {
        startPos = transform.position;
        topPos = startPos + Vector3.up * height;

        // Atribui automaticamente o PlayerLives
        playerLives = FindFirstObjectByType<PlayerLives>();

        Invoke(nameof(StartMovement), startOffset);
    }

    void StartMovement()
    {
        started = true;
    }

    void Update()
    {
        if (!started)
            return;

        timer += Time.deltaTime;

        if (timer < delay)
            return;

        float step = speed * Time.deltaTime;
        if (goingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, topPos, step);
            if (transform.position == topPos)
            {
                goingUp = false;
                timer = 0f;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
            if (transform.position == startPos)
            {
                goingUp = true;
                timer = 0f;
            }
        }
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
