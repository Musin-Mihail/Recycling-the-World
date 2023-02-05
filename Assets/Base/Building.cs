using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject NearBase;
    public GameObject Point;
    public GameObject Beam;

    public GameObject LightBase;

    // int ReadyBuild = 0;
    // int Red;
    // int Yellow;
    // int CostRed = 200;
    // int CostYellow = 20;
    // int AllCostBuild;
    // List<GameObject> MainBase = new List<GameObject>();
    void Start()
    {
        // AllCostBuild = CostRed + CostYellow;
        BaseBuild.StartBuild(gameObject, Beam, Point, NearBase);
        // GetComponent<Delivery>().Search(MainBase, gameObject, "MainBase");
        // StartCoroutine (BaseBuild.Build(MainBase, Red: CostRed, Yellow: CostYellow, AllCost:AllCostBuild));
    }

    public void Build()
    {
        NearBase.GetComponent<Delivery>().AllNearBase.Add(gameObject);
        // transform.parent = Global.Buildings.transform;
        Global.BuildingsList.Add(gameObject);
        Global.BuildingsDiger.Add(gameObject);
        if (gameObject.tag == "Base")
        {
            Global.BuildingsCharge.Add(gameObject);
        }

        LightBase.GetComponent<Light>().enabled = true;
        if (gameObject.tag == "Base")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (gameObject.tag == "Factory")
        {
            GetComponent<SpriteRenderer>().color = new Color32(90, 40, 40, 255);
            Global.CheckFactory = 2;
            Global.ListFactory.Add(gameObject);
        }

        if (gameObject.tag == "Magenta")
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
            Global.CheckMagenta = 2;
            Global.ListMagenta.Add(gameObject);
        }

        Global.NewMaxPosition(transform.position.x, transform.position.y);
    }
}