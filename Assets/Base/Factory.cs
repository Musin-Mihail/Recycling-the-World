using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject NearBase;
    public GameObject NextBase;
    public GameObject Point;
    public GameObject Beam;
    public int ReadyBuild = 0;
    public int BuildRes = 0;
    public int RecRes = 0;
    
    public List<GameObject> ListBase = new List<GameObject>();
    void Start()
    {
        Global.CheckFactory = 1;
        ListBase.Add (gameObject);
        if(NearBase !=null)
        {
            ListBase.Add (NearBase);
            if(NearBase.name != "MainBase")
            {
                NextBase = NearBase.GetComponent<Base>().NearBase;
                CreatingList();
            }
            float BaseDistance = Vector3.Distance(transform.position, NearBase.transform.position);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Point.transform.rotation = Quaternion.LookRotation(Vector3.forward, NearBase.transform.position - Point.transform.position);
            Beam.transform.localPosition = new Vector2(0,BaseDistance/6);
            Beam.transform.localScale = new Vector3(Beam.transform.localScale.x,BaseDistance*2.5f,1);
        }
       StartCoroutine ("Build");
    }

    void Update()
    {
        if (BuildRes == 220)
        {
            GetComponent<SpriteRenderer>().color = new Color32(90,40,40,255);
            ReadyBuild = 1;
            BuildRes = 0;
            Global.Factory = gameObject;
            Global.CheckFactory =  2;
        }
        if (ReadyBuild == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (RecRes >= 220)
        {
            Global.Blue ++;
            RecRes = 0;
            Global.CheckFactory = 2;
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
                var res = Instantiate(Global.ResourceBuild, ListBase[ListBase.Count-1].transform.position, transform.rotation);
                res.GetComponent<ResourceBuild>().ListBase = ListBase;
                res.GetComponent<SpriteRenderer>().color = Color.red;
                res.tag = "Red2";
            }
            else if (Yellow>0)
            {
                Yellow--;
                var res2 = Instantiate(Global.ResourceBuild, ListBase[ListBase.Count-1].transform.position, transform.rotation);
                res2.GetComponent<ResourceBuild>().ListBase = ListBase;
                res2.GetComponent<SpriteRenderer>().color = Color.yellow;
                res2.tag = "Yellow2";
            }
            yield return new WaitForSeconds(0.01f);      
        }
    }
    public IEnumerator Recycling()
    {
        int Red = 200;
        int Yellow = 20;
        while(Red > 0 || Yellow > 0)
        {
            if(Red>0)
            {
                Red --;
                var res = Instantiate(Global.ResourceRecycling, ListBase[ListBase.Count-1].transform.position, transform.rotation);
                res.GetComponent<ResourceRecycling>().ListBase = ListBase;
                res.GetComponent<SpriteRenderer>().color = Color.red;
                res.tag = "Red2";
            }
            else if (Yellow>0)
            {
                Yellow--;
                var res2 = Instantiate(Global.ResourceRecycling, ListBase[ListBase.Count-1].transform.position, transform.rotation);
                res2.GetComponent<ResourceRecycling>().ListBase = ListBase;
                res2.GetComponent<SpriteRenderer>().color = Color.yellow;
                res2.tag = "Yellow2";
            }
            yield return new WaitForSeconds(0.01f);      
        }
    }
    void CreatingList()
    {
        ListBase.Add (NextBase);
        if(NextBase.name != "MainBase")
        {
            NextBase = NextBase.GetComponent<Base>().NearBase;
            CreatingList();
        }      
    }
}