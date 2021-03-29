using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int ReadyBuild = 0;
    public int Red;
    public int Yellow;
    public int Blue;
    int CostRed = 200;
    int CostYellow = 20;
    int AllCostBuild;
    public int busy;
    public List<GameObject> MainBase = new List<GameObject>();
    void Start()
    {
        busy = 0;
        AllCostBuild = CostRed + CostYellow;
        GetComponent<Delivery>().Search(MainBase, gameObject, "MainBase");
        StartCoroutine (BaseBuild.Build(MainBase, Red: CostRed, Yellow: CostYellow, AllCost:AllCostBuild));
    }
    void Update()
    {
        for(int i = 0; i < 2; i++)
        {
            if(Red > 0 || Yellow > 0 || Blue > 0)
            {
                var res = Instantiate(Global.Resource2, transform.position, transform.rotation);
                res.transform.position = new Vector2 (Random.Range(res.transform.position.x-0.3f,res.transform.position.x+0.3f),Random.Range(res.transform.position.y-0.3f,res.transform.position.y+0.3f));
                res.GetComponent<Resource>().ListBase = MainBase;
                if(Red > 0)
                {
                    Red --;
                    res.GetComponent<SpriteRenderer>().color = Color.red;
                    res.tag = "Red2";
                }
                else if(Yellow > 0)
                {
                    Yellow--;
                    res.GetComponent<SpriteRenderer>().color = Color.yellow;
                    res.tag = "Yellow2";
                }
                else if(Blue > 0)
                {
                    Blue--;
                    res.GetComponent<SpriteRenderer>().color = Color.blue;
                    res.tag = "Blue";
                }
            }
        }
    }
}