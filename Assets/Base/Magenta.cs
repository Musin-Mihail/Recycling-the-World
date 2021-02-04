using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magenta : MonoBehaviour
{
    public int RecRes = 0;
    public int Busy = 0;
    public int CostRed = 200;
    public int CostYellow = 20;
    public int CostBlue = 5;
    public int AllCostBuild;
    public List<GameObject> MainBase = new List<GameObject>();
    void Start()
    {
        AllCostBuild = CostRed + CostYellow + CostBlue;
        GetComponent<Delivery>().Search(MainBase, gameObject, "MainBase");
        StartCoroutine (BaseBuild.Build(MainBase, Red: CostRed, Yellow: CostYellow, Blue: CostBlue, AllCost:AllCostBuild));
    }
    public void RecyclingOut()
    {
        RecRes = 0;
        var res = Instantiate(Global.Resource2, transform.position, transform.rotation);
        res.GetComponent<Resource>().ListBase = MainBase;
        res.GetComponent<SpriteRenderer>().color = Color.magenta;
        res.tag = "Magenta";
        Busy = 0;
    }
    public void Recycling()
    {
        Busy = 1;
        StartCoroutine (BaseBuild.Recycling(MainBase, Red: 200, Yellow: 20, Blue: 5, AllCost:AllCostBuild));
    }
}