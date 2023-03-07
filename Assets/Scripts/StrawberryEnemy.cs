using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryEnemy : MonoBehaviour
{
    public Transform target;
    private Rigidbody _rb;
    private float aliveTime;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        transform.LookAt(target);
        _rb.velocity = transform.forward * 2;
        aliveTime = Time.time + 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > aliveTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Blade" || other.name == "Tip" || other.name == "Shuriken")
        {
            Destroy(gameObject);
        }
    }
}
