using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafOn : MonoBehaviour
{
    public ParticleSystem Leaf;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;

        Leaf.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
