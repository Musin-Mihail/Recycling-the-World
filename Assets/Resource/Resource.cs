using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int count = 0;
    public List<GameObject> ListBase = new List<GameObject>();

    void Start()
    {
        count = ListBase.Count - 2;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, ListBase[count].transform.position, 0.5f);
        if (transform.position == ListBase[count].transform.position)
        {
            if (ListBase[count].name != "MainBase")
            {
                transform.position = new Vector2(Random.Range(ListBase[count].transform.position.x - 0.4f, ListBase[count].transform.position.x + 0.4f), Random.Range(ListBase[count].transform.position.y - 0.4f, ListBase[count].transform.position.y + 0.4f));
                count--;
            }
            else
            {
                if (tag == "Red2")
                {
                    Global.RedBase++;
                }
                else if (tag == "Yellow2")
                {
                    Global.YellowBase++;
                }
                else if (tag == "Blue")
                {
                    Global.BlueBase++;
                }
                else if (tag == "Magenta")
                {
                    Global.BaseMagenta++;
                }

                Destroy(gameObject);
            }
        }
    }
}