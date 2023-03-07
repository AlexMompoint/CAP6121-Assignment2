using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PeachEnemyScript : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponentInChildren<Rigidbody>();
        
    }

    bool isLookingAt()
    {
        if(Vector3.Dot(transform.forward,Vector3.Normalize(target.position - transform.position)) > 0.9)
            return true;
        return false; 
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (isLookingAt() && Vector3.Magnitude(_rb.velocity) < 0.05)
        {
            _rb.AddForce(10 * transform.forward,ForceMode.Impulse);
        } else if((!isLookingAt() && Vector3.Magnitude(_rb.velocity) < 0.05) && Vector3.Magnitude(_rb.angularVelocity) < 0.05)
        {
            transform.LookAt(target.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Blade" || other.name == "Tip" || other.name == "Shuriken")
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
