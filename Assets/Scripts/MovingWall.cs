using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Vector3 direction = Vector3.left;  // Direção do movimento
    public float distance = 3f;               // Distância total de ida e volta
    public float speed = 2f;                  // Velocidade da parede

    private Vector3 startPos;
    private Vector3 endPos;
    private bool goingForward = true;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + direction.normalized * distance;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (goingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
            if (transform.position == endPos)
                goingForward = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
            if (transform.position == startPos)
                goingForward = true;
        }
    }
}
