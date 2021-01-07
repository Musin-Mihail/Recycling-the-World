using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
//Уничтожение блоков
    public GameObject LaserPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Block" )
        {
            var Distance = Vector3.Distance(transform.parent.position, other.transform.position);
            transform.localScale = new Vector3(1,Distance/1.5f,1);
            transform.localPosition = new Vector2(0,Distance/10.7f);
            if(other.transform.position.x <1.25f)
            {
                LaserPoint.transform.position = other.transform.position;
                Invoke("Resetpoint",0.5f);
            }
        }
    }
    void Resetpoint()
    {
        LaserPoint.transform.localPosition = new Vector3(0,0,0);
    }
}