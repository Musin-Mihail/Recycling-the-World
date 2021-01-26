using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magenta : MonoBehaviour
{
    public GameObject NearBase;
    public GameObject Point;
    public GameObject Beam;
    public int BuildRes = 0;
    public int RecRes = 0;
    public int Busy = 0;
    public List<GameObject> MainBase = new List<GameObject>();
    void Start()
    {
        BaseBuild.StartBuild(gameObject, Beam, Point, NearBase);
        GetComponent<Delivery>().Search(MainBase, gameObject, "MainBase");
        StartCoroutine (BaseBuild.Build(MainBase, Red: 200, Yellow: 20, Blue: 5));
    }
    void Update()
    {
        if (BuildRes == 225)
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
            NearBase.GetComponent<Delivery>().AllNearBase.Add (gameObject);
            BuildRes = 0;
            Global.ListMagenta.Add(gameObject);
            Global.CheckMagenta =  2;
        }
        if (RecRes == 225)
        {
            RecRes = 0;
            var res = Instantiate(Global.Resource2, transform.position, transform.rotation);
            res.GetComponent<Resource>().ListBase = MainBase;
            res.GetComponent<SpriteRenderer>().color = Color.magenta;
            res.tag = "Magenta";
            Busy = 0;
        }
    }
    public void Recycling()
    {
        Busy = 1;
        StartCoroutine (BaseBuild.Recycling(MainBase, Red: 200, Yellow: 20, Blue: 5));
    }
}