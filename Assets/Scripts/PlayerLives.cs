using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Para usar a classe Image

public class PlayerLives : MonoBehaviour
{
    public int totalLives = 3; // Número total de vidas
    public float invulnerableTime = 1f; // Tempo em segundos que o jogador fica invulnerável após perder uma vida
    private float invulnerableTimer = 0f; // Controla o tempo restante de invulnerabilidade
    public GameOverManager gameOverManager; // Referência ao GameOverManager

    public Sprite fullHeart;  // Coração cheio (vermelho)
    public Sprite emptyHeart; // Coração vazio (cinza)
    public Image[] Hearts;   // Array de imagens de corações na tela

    void Update()
    {
        // Reduz o tempo de invulnerabilidade a cada frame
        if (invulnerableTimer > 0f)
        {
            invulnerableTimer -= Time.deltaTime;
        }
    }

    // Este método deve ser chamado sempre que o jogador morrer
    public void TakeDamage()
    {
        // Não faz nada se o jogador estiver invulnerável
        if (invulnerableTimer > 0f)
            return;

        totalLives--; // Decrementa uma vida
        UpdateHearts(); // Atualiza os corações

        invulnerableTimer = invulnerableTime;

        // Se as vidas chegarem a 0, chama a tela de Game Over
        if (totalLives <= 0)
        {
            gameOverManager.GameOver(); // Mostra a tela de Game Over
        }
    }

    // Este método pode ser chamado no início ou em outro lugar para resetar as vidas
    public void ResetLives()
    {
        totalLives = 3;
        UpdateHearts();
    }

    // Atualiza os corações na tela
    void UpdateHearts()
    {
        // Atualiza os corações de acordo com o número de vidas
        for (int i = 0; i < Hearts.Length; i++)  // Alterado de 3 para usar o tamanho do array Hearts
        {
            if (i < totalLives)
            {
                // Corações preenchidos (vermelho)
                Hearts[i].sprite = fullHeart;
            }
            else
            {
                // Corações vazios (cinza)
                Hearts[i].sprite = emptyHeart;
            }
        }
    }
}
