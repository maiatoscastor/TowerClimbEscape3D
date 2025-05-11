using UnityEngine;

public class Gerar : MonoBehaviour
{
    public GameObject[] degraus; // Os teus degraus
    public GameObject[] prefabsArmadilhas; // Vários tipos de armadilhas
    public float chanceDoObstaculo = 0.5f;

    void Start()
    {
        foreach (GameObject degrau in degraus)
        {
            if (Random.value < chanceDoObstaculo)
            {
                // Escolhe aleatoriamente um prefab do array
                GameObject armadilhaEscolhida = prefabsArmadilhas[Random.Range(0, prefabsArmadilhas.Length)];

                Renderer obstaculoRenderer = armadilhaEscolhida.GetComponent<Renderer>();
                Vector3 obstaculoSize = obstaculoRenderer.bounds.size;

                Renderer degrauRenderer = degrau.GetComponent<Renderer>();
                if (degrauRenderer == null) continue;

                Bounds bounds = degrauRenderer.bounds;

                float minX = bounds.min.x + obstaculoSize.x / 2f;
                float maxX = bounds.max.x - obstaculoSize.x / 2f;
                float minZ = bounds.min.z + obstaculoSize.z / 2f;
                float maxZ = bounds.max.z - obstaculoSize.z / 2f;
                float posX = Random.Range(minX, maxX);
                float posZ = Random.Range(minZ, maxZ);
                float posY = bounds.max.y + 0.4f;

                Vector3 posicao = new Vector3(posX, posY, posZ);

                Instantiate(armadilhaEscolhida, posicao, Quaternion.identity);
            }
        }
    }
}
