using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI recordeText;

    private float elapsedTime;
    private bool isRunning = true;

    void Start()
    {
        // Mostrar recorde guardado
        float recorde = PlayerPrefs.GetFloat("MelhorTempo", 0f);
        recordeText.text = $"Recorde: {FormatTime(recorde)}";
    }

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;
        timerText.text = $"Tempo: {FormatTime(elapsedTime)}";
    }

    public void StopTimer()
    {
        isRunning = false;

        // Verifica se foi um novo recorde
        float melhorTempo = PlayerPrefs.GetFloat("MelhorTempo", 0f);
        if (elapsedTime > melhorTempo)
        {
            PlayerPrefs.SetFloat("MelhorTempo", elapsedTime);
            recordeText.text = $"Recorde: {FormatTime(elapsedTime)}";
            PlayerPrefs.Save();
        }
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}