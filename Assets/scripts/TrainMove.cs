using UnityEngine;

public class TrainMove : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector3 initialPosition;
    public float timeLimit = 5.0f;
    private float timer = 0.0f;
    public ParticleSystem party;



    void Start()
    {
        initialPosition = transform.position;

        party.Play();



    }

    void Update()
    {
        timer += Time.deltaTime;

        transform.Translate(-speed * Time.deltaTime, 0, -speed * Time.deltaTime, Space.World);



        if (timer > timeLimit)
        {
            transform.position = initialPosition;
            timer = 0.0f;
        }


    }
}