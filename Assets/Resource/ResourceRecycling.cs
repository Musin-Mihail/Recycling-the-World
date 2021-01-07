using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRecycling : MonoBehaviour
{
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
                    ListBase[count].GetComponent<Factory>().RecRes ++;
                    Destroy(gameObject);
                }
            }
        }
    }
}