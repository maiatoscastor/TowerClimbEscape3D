using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAleatorio : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ModoAleatorio");
    }
}
