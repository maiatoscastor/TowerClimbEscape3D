using UnityEngine;

public class ActivateLavaTrigger : MonoBehaviour
{
    public LavaRise lava;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true; // Marca como já ativado
            lava.isActive = true;
            Debug.Log("🔥 Lava ativada!");

            // (Opcional) Desativa o trigger completamente
            GetComponent<Collider>().enabled = false;
        }
    }
}