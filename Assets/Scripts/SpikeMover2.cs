using UnityEngine;

public class SpikeMover2 : MonoBehaviour
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

    void Start()
    {
        startPos = transform.position;
        topPos = startPos + Vector3.up * height;

        // Aplica delay inicial antes de ativar o movimento
        StartCoroutine(DelayedStart());
    }

    System.Collections.IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(startOffset);
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
}
