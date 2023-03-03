using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaEnemy : MonoBehaviour
{
    public GameObject item;
    public Transform center;
    public bool cw = true;
    public float rotateSpeed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        item.transform.RotateAround(center.position, Vector3.up, (cw) ? rotateSpeed * -1 : rotateSpeed);
    }
}
