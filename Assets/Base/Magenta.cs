using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magenta : MonoBehaviour
{
    public GameObject NearBase;
    public GameObject NextBase;
    public GameObject Point;
    public GameObject Beam;
    public int ReadyBuild = 0;
    public int BuildRes = 0;
    public int RecRes = 0;
    public int Busy = 0;
    public List<GameObject> MainBase = new List<GameObject>();
    void Start()
    {
        
        if(NearBase !=null)
        {
            GetComponent<Delivery>().AllNearBase.Add (NearBase);
            if(NearBase.name != "MainBase")
            {
                NextBase = NearBase.GetComponent<Base>().NearBase;
            }
            float BaseDistance = Vector3.Distance(transform.position, NearBase.transform.position);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Point.transform.rotation = Quaternion.LookRotation(Vector3.forward, NearBase.transform.position - Point.transform.position);
            Beam.transform.localPosition = new Vector2(0,BaseDistance/6);
            Beam.transform.localScale = new Vector3(Beam.transform.localScale.x,BaseDistance*2.5f,1);
        }
        GetComponent<Delivery>().Search(MainBase, gameObject, "MainBase");
        StartCoroutine ("Build");
    }
    void Update()
    {
        if (BuildRes == 225)
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
            NearBase.GetComponent<Delivery>().AllNearBase.Add (gameObject);
            ReadyBuild = 1;
            BuildRes = 0;
            Global.CheckMagenta =  2;
            Global.ListMagenta.Add(gameObject);
        }
        if (ReadyBuild == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (RecRes == 225)
        {
            Global.BaseMagenta ++;
            RecRes = 0;
            Busy = 0;
        }
    }
    IEnumerator Build()
    {
        int Red = 200;
        int Yellow = 20;
        int Blue = 5;
        while(Red > 0 || Yellow > 0 || Blue > 0)
        {
            if(Red > 0)
            {
                Red --;
                var res = Instantiate(Global.ResourceBuild, MainBase[0].transform.position, transform.rotation);
                res.GetComponent<ResourceBuild>().ListBase = MainBase;
                res.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (Yellow > 0)
            {
                Yellow--;
                var res2 = Instantiate(Global.ResourceBuild, MainBase[0].transform.position, transform.rotation);
                res2.GetComponent<ResourceBuild>().ListBase = MainBase;
                res2.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (Blue > 0)
            {
                Blue--;
                var res2 = Instantiate(Global.ResourceBuild, MainBase[0].transform.position, transform.rotation);
                res2.GetComponent<ResourceBuild>().ListBase = MainBase;
                res2.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            yield return new WaitForSeconds(0.01f);      
        }
    }
    public IEnumerator Recycling()
    {
        Busy = 1;
        int Red = 200;
        int Yellow = 20;
        int Blue = 5;
        while(Red > 0 || Yellow > 0 || Blue > 0)
        {
            if(Red>0)
            {
                Red --;
                var res = Instantiate(Global.ResourceRecycling, MainBase[0].transform.position, transform.rotation);
                res.GetComponent<ResourceRecycling>().ListBase = MainBase;
                res.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (Yellow > 0)
            {
                Yellow--;
                var res2 = Instantiate(Global.ResourceRecycling, MainBase[0].transform.position, transform.rotation);
                res2.GetComponent<ResourceRecycling>().ListBase = MainBase;
                res2.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (Blue > 0)
            {
                Blue--;
                var res2 = Instantiate(Global.ResourceRecycling, MainBase[0].transform.position, transform.rotation);
                res2.GetComponent<ResourceRecycling>().ListBase = MainBase;
                res2.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            yield return new WaitForSeconds(0.01f);      
        }
    }
}