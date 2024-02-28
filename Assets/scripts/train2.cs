using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // Save the start position
        InvokeRepeating(nameof(ResetPosition), 5.0f, 5.0f); // Reset position every 5 seconds
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);
    }

    // Reset the object position
    void ResetPosition()
    {
        transform.position = startPosition;
    }
}