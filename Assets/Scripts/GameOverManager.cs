using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button exitButton;

    public Camera mainCamera;  // Referência à câmera principal
    public Camera gameOverCamera;  // Referência à nova câmera do Game Over

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);

        mainCamera.gameObject.SetActive(true);
        gameOverCamera.gameObject.SetActive(false);

        gameOverPanel.SetActive(false);  // Inicialmente o painel de game over está invisível
    }

    public void GameOver()
    {
        // Para o cronómetro e guarda recorde, se for o melhor
        FindFirstObjectByType<GameTimer>()?.StopTimer();

        // Desabilita a câmera principal
        mainCamera.gameObject.SetActive(false);
        
        // Ativa a câmera do Game Over
        gameOverCamera.gameObject.SetActive(true);

        // Mostra o painel de Game Over
        gameOverPanel.SetActive(true);
        gameOverText.text = "Game Over!";

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    void RestartGame()
    {
        // Reinicia a cena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 

        // Reabilita a câmera principal
        mainCamera.gameObject.SetActive(true);
        
        // Desativa a câmera do Game Over
        gameOverCamera.gameObject.SetActive(false);

        // Esconde o painel de Game Over após reiniciar
        gameOverPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void ExitGame()
    {
        Application.Quit();  // Fecha o jogo
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Caso esteja no Editor
        #endif
    }
}
