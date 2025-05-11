using UnityEngine;
using System.Collections;

public class SpikeGroupController : MonoBehaviour
{
    [System.Serializable]
    public class SpikeInfo
    {
        public Transform spikeTransform;
        public float startOffset;
        public float height = 1.5f;
    }

    public SpikeInfo[] spikes;
    public float speed = 2f;
    public float delay = 1f;

    private BoxCollider damageCollider;
    private float baseY;

    void Start()
    {
        damageCollider = GetComponent<BoxCollider>();
        damageCollider.isTrigger = true;
        baseY = transform.position.y;

        foreach (var spike in spikes)
        {
            StartCoroutine(SpikeCycle(spike));
        }
    }

    void Update()
    {
        AtualizarCollider();
    }

    IEnumerator SpikeCycle(SpikeInfo spike)
    {
        Vector3 startPos = spike.spikeTransform.position;
        Vector3 topPos = startPos + Vector3.up * spike.height;
        bool goingUp = true;
        float timer = 0f;

        yield return new WaitForSeconds(spike.startOffset);

        while (true)
        {
            if (timer < delay)
            {
                timer += Time.deltaTime;
                yield return null;
                continue;
            }

            float step = speed * Time.deltaTime;

            if (goingUp)
            {
                spike.spikeTransform.position = Vector3.MoveTowards(spike.spikeTransform.position, topPos, step);
                if (spike.spikeTransform.position == topPos)
                {
                    goingUp = false;
                    timer = 0f;
                }
            }
            else
            {
                spike.spikeTransform.position = Vector3.MoveTowards(spike.spikeTransform.position, startPos, step);
                if (spike.spikeTransform.position == startPos)
                {
                    goingUp = true;
                    timer = 0f;
                }
            }

            yield return null;
        }
    }

    void AtualizarCollider()
    {
        if (spikes.Length == 0 || damageCollider == null) return;

        float highestY = float.MinValue;
        float lowestY = float.MaxValue;

        foreach (var spike in spikes)
        {
            float y = spike.spikeTransform.position.y;
            if (y > highestY) highestY = y;
            if (y < lowestY) lowestY = y;
        }

        float centerY = (highestY + lowestY) / 2f;
        float height = (highestY - lowestY) + 0.2f; // margem extra

        Vector3 center = transform.InverseTransformPoint(new Vector3(transform.position.x, centerY, transform.position.z));
        damageCollider.center = new Vector3(0, center.y - baseY, 0);
        damageCollider.size = new Vector3(1, height, 1); // ajusta X e Z conforme necessário
    }
}