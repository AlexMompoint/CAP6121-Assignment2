using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AvocadoEnemy : MonoBehaviour
{
    public GameObject cover;
    public Transform center;
    public Transform target;
    public GameObject prefab;
    public Transform[] spawns = new Transform[5];

    private float idleTime = 10;
    private float nextSwap;
    private float lastSpawn;
    private bool changing = true;
    private bool closed = true;
    public float rotateSpeed = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        nextSwap = Time.time + idleTime;
        lastSpawn = Time.time;
    }

    bool rotateCover180()
    {
        cover.transform.RotateAround(center.position, Vector3.up, (closed) ? rotateSpeed  : rotateSpeed * -1);
        Debug.Log($"{cover.transform.eulerAngles}");
        if(cover.transform.localEulerAngles.y > 359.0f)
        {
            return true;
        } else if (cover.transform.localEulerAngles.y < 180.0f)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSwap)
        {
            changing = true;
        } else if(Time.time > lastSpawn +  1.0f && !closed && !changing)
        {
            lastSpawn = Time.time;
            int RandomNum = Random.Range(0, 5);
            Instantiate(prefab, spawns[RandomNum].position, transform.rotation);
        } else if(!closed && !changing) {
            Transform tempRot = transform;
            tempRot.LookAt(target, Vector3.up);
            transform.rotation = Quaternion.Euler(0, tempRot.eulerAngles.y, 0);
        }

    }

    private void FixedUpdate()
    {
        if(changing && rotateCover180())
        {
            Debug.Log("Swapped");
            closed ^= true;
            nextSwap = Time.time + idleTime;
            changing = false;
        }
    }
}
