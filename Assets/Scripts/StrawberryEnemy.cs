using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryEnemy : MonoBehaviour
{
    public Transform target;
    public Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        transform.LookAt(target);
        _rb.velocity = transform.forward * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
