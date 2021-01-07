using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBlock : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(gameObject);
        }
    }
}