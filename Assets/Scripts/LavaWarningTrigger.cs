using UnityEngine;

public class LavaWarningTrigger : MonoBehaviour
{
    public GameObject avisoUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MostrarAviso());
        }
    }

    private System.Collections.IEnumerator MostrarAviso()
    {
        avisoUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        avisoUI.SetActive(false);
    }
}
