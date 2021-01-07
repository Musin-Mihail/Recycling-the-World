using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public GameObject Target;
    // Rigidbody2D rb;

    // void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    // }
    void Update()
    {
        if(Target !=null)
        {
            if(Target.name != "MainBase")
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 0.5f);
                if (transform.position == Target.transform.position)
                {
                    Target = Target.GetComponent<Base>().NearBase;
                    transform.position = new Vector2 (Random.Range(transform.position.x-0.3f,transform.position.x+0.3f),Random.Range(transform.position.y-0.3f,transform.position.y+0.3f));
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 0.5f);
                if (transform.position == Target.transform.position)
                {
                    if(tag == "Red2")
                    {
                        Global.RedBase ++;
                        Destroy(gameObject);
                    }
                    else if(tag == "Yellow2")
                    {
                        Global.YellowBase ++;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.name == "Base")
    //     {
    //         Target = other.GetComponent<Base>().NearBase;
    //     }
    // }
}