using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
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
        StartCoroutine (BaseBuild.Build(MainBase, Red: 200, Yellow: 20));
    }
    void Update()
    {
        if (BuildRes == 220)
        {
            GetComponent<SpriteRenderer>().color = new Color32(90,40,40,255);
            NearBase.GetComponent<Delivery>().AllNearBase.Add (gameObject);
            BuildRes = 0;
            Global.ListFactory.Add(gameObject);
            Global.CheckFactory =  2;
        }
        if (RecRes == 220)
        {
            RecRes = 0;
            var res = Instantiate(Global.Resource2, transform.position, transform.rotation);
            res.GetComponent<Resource>().ListBase = MainBase;
            res.GetComponent<SpriteRenderer>().color = Color.blue;
            res.tag = "Blue";
            Busy = 0;
        }
    }
    public void Recycling()
    {
        Busy = 1;
        StartCoroutine (BaseBuild.Recycling(MainBase, Red: 200, Yellow: 20));
    }
}