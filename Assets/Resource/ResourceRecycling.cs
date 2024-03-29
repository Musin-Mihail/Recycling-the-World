using System.Collections.Generic;
using UnityEngine;

public class ResourceRecycling : MonoBehaviour
{
    int count = 0;
    public List<GameObject> ListBase = new List<GameObject>();

    void Update()
    {
        if (ListBase[count] != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, ListBase[count].transform.position, 0.5f);
            if (transform.position == ListBase[count].transform.position)
            {
                if (count != ListBase.Count - 1)
                {
                    transform.position = new Vector2(Random.Range(ListBase[count].transform.position.x - 0.4f, ListBase[count].transform.position.x + 0.4f), Random.Range(ListBase[count].transform.position.y - 0.4f, ListBase[count].transform.position.y + 0.4f));
                    count++;
                }
                else
                {
                    if (gameObject.name == "EndRes")
                    {
                        if (ListBase[count].tag == "Factory")
                        {
                            ListBase[count].GetComponent<Factory>().RecyclingOut();
                        }
                        else if (ListBase[count].tag == "Magenta")
                        {
                            ListBase[count].GetComponent<Magenta>().RecyclingOut();
                        }
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}