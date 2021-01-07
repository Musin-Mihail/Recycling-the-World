using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuild : MonoBehaviour
{
    // public GameObject Target;
    int count;
    public List<GameObject> ListBase = new List<GameObject>();
    void Start()
    {
        count = ListBase.Count-1;
    }
    void Update()
    {
        if(ListBase[count]!=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, ListBase[count].transform.position, 0.5f);
            if (transform.position == ListBase[count].transform.position)
            {
                if(count!=0)
                {
                    
                    transform.position = new Vector2 (Random.Range(ListBase[count].transform.position.x-0.4f,ListBase[count].transform.position.x+0.4f),Random.Range(ListBase[count].transform.position.y-0.4f,ListBase[count].transform.position.y+0.4f));
                    count--;
                }
                else
                {
                    if(ListBase[count].GetComponent<Base>())
                    {
                        ListBase[count].GetComponent<Base>().BuildRes ++;
                    }
                    else
                    {
                        ListBase[count].GetComponent<Factory>().BuildRes ++;
                    }
                    
                    Destroy(gameObject);
                }
            }

            // if(ListBase[count].name != "MainBase")
            // {
            //     transform.position = Vector3.MoveTowards(transform.position, ListBase[count].transform.position, 0.5f);
            //     if (transform.position == ListBase[count].transform.position)
            //     {
            //         Target = Target.GetComponent<Base>().NearBase;
            //         transform.position = new Vector2 (Random.Range(transform.position.x-0.3f,transform.position.x+0.3f),Random.Range(transform.position.y-0.3f,transform.position.y+0.3f));
            //     }
            // }
            // else
            // {
            //     transform.position = Vector3.MoveTowards(transform.position, ListBase[count].transform.position, 0.5f);
            //     if (transform.position == ListBase[count].transform.position)
            //     {
            //         if(tag == "Red2")
            //         {
            //             Global.RedBase ++;
            //             Destroy(gameObject);
            //         }
            //         else if(tag == "Yellow2")
            //         {
            //             Global.YellowBase ++;
            //             Destroy(gameObject);
            //         }
            //     }
            // }
        }
    }
}