using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    float Speed = 0.2f;
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x > Global._minMoveX)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, Speed);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < Global._maxMoveX)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, Speed);
        }
        if (Input.GetKey(KeyCode.W) && transform.position.y < Global._maxMoveY)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Speed);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y > Global._minMoveY)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, Speed);
        }
    }
}
