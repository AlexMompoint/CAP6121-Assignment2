using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float forwardVelocity = 3.0f;
    private bool rot = true;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * forwardVelocity;
        _rb.angularVelocity = new Vector3(0, rotationSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
