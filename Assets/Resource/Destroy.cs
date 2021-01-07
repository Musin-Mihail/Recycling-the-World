using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    Rigidbody2D rb;
    int layerMask = 1 << 8;
    int layerMask2 = 1 << 10;
    int layerMask3;
    float BaseDistance;
    public float DistancePlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        layerMask3 = layerMask | layerMask2;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.name == "Vacuum")
        {
            if(Global.storageCount < UpdatePlayer.storageCountMax && Global.Energy>0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, other.transform.position-transform.position,20,layerMask3);
                if(hit && hit.collider.name == "Body")
                {
                    rb.gravityScale = 0;
                    rb.velocity = new Vector3(0,0,0);
                    BaseDistance = Vector3.Distance(transform.position, other.transform.position);
                    if(BaseDistance>1.8f)
                    {
                        rb.transform.position = Vector3.Lerp(transform.position, other.transform.position, Time.deltaTime*4);
                    }
                }
                else
                {
                    rb.gravityScale = 1; 
                }
            }
            else
            {
                rb.gravityScale = 1; 
            }
        } 
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        rb.gravityScale = 1;
    }
}