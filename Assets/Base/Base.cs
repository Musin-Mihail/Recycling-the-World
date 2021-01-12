using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public GameObject NearBase;
    public GameObject NextBase;
    public GameObject Point;
    public GameObject Beam;
    public int ReadyBuild = 0;
    public int BuildRes = 0;
    public GameObject LightBase;
    int Red;
    int Yellow;
    public List<GameObject> ListBase = new List<GameObject>();
    public List<GameObject> AllNearBase = new List<GameObject>();
    public List<GameObject> AllFactory = new List<GameObject>();
    public List<GameObject> TestFactory = new List<GameObject>();
    public int Test = 0;
    void Start()
    {
        ListBase.Add (gameObject);
        if(NearBase !=null)
        {
            ListBase.Add (NearBase);
            AllNearBase.Add (NearBase);
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
        if (Test == 1)
        {
            Test = 0;
            SearchFactory(TestFactory, gameObject);
        }
        for(int i = 0; i < 2; i++)
        {
            if(Red > 0)
            {
                Red --;
                var res = Instantiate(Global.Resource2, transform.position, transform.rotation);
                res.transform.position = new Vector2 (Random.Range(res.transform.position.x-0.3f,res.transform.position.x+0.3f),Random.Range(res.transform.position.y-0.3f,res.transform.position.y+0.3f));
                res.GetComponent<SpriteRenderer>().color = Color.red;
                res.GetComponent<Resource>().Target = NearBase;
                res.tag = "Red2";
            }
            else if(Yellow > 0)
            {
                Yellow--;
                var res2 = Instantiate(Global.Resource2, transform.position, transform.rotation);
                res2.transform.position = new Vector3 
                (Random.Range(res2.transform.position.x-0.3f,res2.transform.position.x+0.3f),Random.Range(res2.transform.position.y-0.3f,res2.transform.position.y+0.3f),res2.transform.position.z);
                res2.GetComponent<SpriteRenderer>().color = Color.yellow;
                res2.GetComponent<Resource>().Target = NearBase;
                res2.tag = "Yellow2";
            }
        }
        if (BuildRes == 220)
        {
            if(NearBase.name  != "MainBase")
            {
                NearBase.GetComponent<Base>().AllNearBase.Add (gameObject);
            }
            GetComponent<SpriteRenderer>().color = Color.white;
            transform.parent = Global.Buildings.transform;
            LightBase.GetComponent<Light>().enabled = true;
            
            ReadyBuild = 1;
            BuildRes = 0;
        }
        if (ReadyBuild == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
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
    void SearchFactory(List<GameObject> test, GameObject deleted)
    {
        if(AllFactory.Count > 0)
        {
            test.Add (AllFactory[0]);
            test.Add (gameObject);
        }
        else 
        {
            int count = 0;
            foreach (var Nbase in AllNearBase)
            {
                if(Nbase.name != "MainBase")
                {
                    if (Nbase.name != deleted.name)
                    {
                        count++;
                        Nbase.GetComponent<Base>().SearchFactory(test, gameObject);
                    }  
                }
            }
            if(count>0)
            {
                test.Add (gameObject);
            }
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