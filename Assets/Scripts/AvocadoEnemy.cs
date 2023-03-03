using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoEnemy : MonoBehaviour
{
    public GameObject item;
    public Transform center;
    private bool closed = true;
    public float rotateSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void rotateCover180()
    {
        item.transform.RotateAround(center.position, Vector3.up, (closed) ? rotateSpeed  : rotateSpeed * -1);
        if(item.transform.eulerAngles.y > 359.0f)
        {
            closed = false;
        } else if (item.transform.eulerAngles.y < 180.0f)
        {
            closed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
            rotateCover180();
    }
}
