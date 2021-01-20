using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public GameObject NearBase;
    public GameObject Point;
    public GameObject Beam;
    public int ReadyBuild = 0;
    public int BuildRes = 0;
    public GameObject LightBase;
    int Red;
    int Yellow;
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
        for(int i = 0; i < 2; i++)
        {
            if(Red > 0)
            {
                Red --;
                var res = Instantiate(Global.Resource2, transform.position, transform.rotation);
                res.transform.position = new Vector2 (Random.Range(res.transform.position.x-0.3f,res.transform.position.x+0.3f),Random.Range(res.transform.position.y-0.3f,res.transform.position.y+0.3f));
                res.GetComponent<SpriteRenderer>().color = Color.red;
                res.GetComponent<Resource>().ListBase = MainBase;
                res.tag = "Red2";
            }
            else if(Yellow > 0)
            {
                Yellow--;
                var res2 = Instantiate(Global.Resource2, transform.position, transform.rotation);
                res2.transform.position = new Vector3 (Random.Range(res2.transform.position.x-0.3f,res2.transform.position.x+0.3f),Random.Range(res2.transform.position.y-0.3f,res2.transform.position.y+0.3f),res2.transform.position.z);
                res2.GetComponent<SpriteRenderer>().color = Color.yellow;
                res2.GetComponent<Resource>().ListBase = MainBase;
                res2.tag = "Yellow2";
            }
        }
        if (BuildRes == 220)
        {
            NearBase.GetComponent<Delivery>().AllNearBase.Add (gameObject);
            GetComponent<SpriteRenderer>().color = Color.white;
            transform.parent = Global.Buildings.transform;
            LightBase.GetComponent<Light>().enabled = true;
            ReadyBuild = 1;
            BuildRes = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && ReadyBuild == 1)
        {
            for(int i = 0; i < Global.storage; i++)
            {
                if(Global.Red > 0)
                {
                    Red ++;
                    Global.Red --;
                }
                else if(Global.Yellow > 0)
                {
                    Yellow++;
                    Global.Yellow --;
                }
            }
            if (Global.EnergyCount<UpdatePlayer.EnergyCountMax)
            {
                Global.EnergyCount +=50;
            }
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
                res.tag = "Red2";
            }
            else if (Yellow>0)
            {
                Yellow--;
                var res2 = Instantiate(Global.ResourceBuild, MainBase[0].transform.position, transform.rotation);
                res2.GetComponent<ResourceBuild>().ListBase = MainBase;
                res2.GetComponent<SpriteRenderer>().color = Color.yellow;
                res2.tag = "Yellow2";
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}