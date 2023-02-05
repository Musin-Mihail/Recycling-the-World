using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public int Busy = 0;
    int CostRed = 200;
    int CostYellow = 20;
    int AllCostBuild;
    public List<GameObject> MainBase = new List<GameObject>();

    void Start()
    {
        AllCostBuild = CostRed + CostYellow;
        GetComponent<Delivery>().Search(MainBase, gameObject, "MainBase");
        StartCoroutine(BaseBuild.Build(MainBase, Red: CostRed, Yellow: CostYellow, AllCost: AllCostBuild));
    }

    public void RecyclingOut()
    {
        var res = Instantiate(Global.Resource2, transform.position, transform.rotation);
        res.GetComponent<Resource>().ListBase = MainBase;
        res.GetComponent<SpriteRenderer>().color = Color.blue;
        res.tag = "Blue";
        Busy = 0;
    }

    public void Recycling()
    {
        Busy = 1;
        StartCoroutine(BaseBuild.Recycling(MainBase, Red: 200, Yellow: 20, AllCost: AllCostBuild));
    }
}