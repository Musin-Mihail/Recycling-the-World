using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject NearBase;
    public GameObject Point;
    public GameObject Beam;
    public int ReadyBuild = 0;
    public int BuildRes = 0;
    public int RecRes = 0;
    public int Busy = 0;
    public List<GameObject> MainBase = new List<GameObject>();
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        float BaseDistance = Vector3.Distance(transform.position, NearBase.transform.position);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Point.transform.rotation = Quaternion.LookRotation(Vector3.forward, NearBase.transform.position - Point.transform.position);
        Beam.transform.localPosition = new Vector2(0,BaseDistance/6);
        Beam.transform.localScale = new Vector3(Beam.transform.localScale.x,BaseDistance*2.5f,1);
        GetComponent<Delivery>().Search(MainBase, gameObject, "MainBase");
        StartCoroutine ("Build");
    }
    void Update()
    {
        if (BuildRes == 220)
        {
            GetComponent<SpriteRenderer>().color = new Color32(90,40,40,255);
            NearBase.GetComponent<Delivery>().AllNearBase.Add (gameObject);
            ReadyBuild = 1;
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
    IEnumerator Build()
    {
        int Red = 200;
        int Yellow = 20;
        while(Red > 0 || Yellow > 0)
        {
            if(Red>0)
            {
                Red --;
                var res = Instantiate(Global.ResourceBuild, MainBase[0].transform.position, transform.rotation);
                res.GetComponent<ResourceBuild>().ListBase = MainBase;
                res.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (Yellow>0)
            {
                Yellow--;
                var res2 = Instantiate(Global.ResourceBuild, MainBase[0].transform.position, transform.rotation);
                res2.GetComponent<ResourceBuild>().ListBase = MainBase;
                res2.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            yield return new WaitForSeconds(0.01f);      
        }
    }
    public IEnumerator Recycling()
    {
        Busy = 1;
        int Red = 200;
        int Yellow = 20;
        while(Red > 0 || Yellow > 0)
        {
            if(Red>0)
            {
                Red --;
                var res = Instantiate(Global.ResourceRecycling, MainBase[0].transform.position, transform.rotation);
                res.GetComponent<ResourceRecycling>().ListBase = MainBase;
                res.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (Yellow>0)
            {
                Yellow--;
                var res2 = Instantiate(Global.ResourceRecycling, MainBase[0].transform.position, transform.rotation);
                res2.GetComponent<ResourceRecycling>().ListBase = MainBase;
                res2.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            yield return new WaitForSeconds(0.01f);      
        }
    }
}