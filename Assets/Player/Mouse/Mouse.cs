using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
// Курсор и установка зданий
    public int check = 0;
    public GameObject Base2;
    public GameObject Factory;
    public GameObject Magenta;
    public GameObject EmptyBase;
    public GameObject BBase;
    public float BaseDistance;
    int CostRed;
    int CostYellow;
    int CostBlue;
    int CostMagenta;
    public SpriteRenderer EmptyBaseM;
    int layerMask = 1 << 8;//Блоки
    int layerMask2 = 1 << 9;//База
    int layerMask3;
    List<string> _nameBuilders;
    public Vector2 target;
    Vector2 _oldVector2;
    float _distans;
    enum BuildingName {Wait, Base, Factory, Magenta};
    void Start()
    {
        _nameBuilders = new List<string>();
//Объединение слоёв
        layerMask3 = layerMask | layerMask2;
    }
    void Update()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (check != 0)
        {
            EmptyBase.transform.position = target;
            SearchBase();
            transform.position = new Vector3 (1000, 1000, -3);
            BaseDistance = Vector3.Distance(BBase.transform.position, EmptyBase.transform.position);
            if(BaseDistance > 20 || Global.RedBase < CostRed || Global.YellowBase < CostYellow || Global.BlueBase < CostBlue)
            {
                Debug.Log(BaseDistance);
                EmptyBaseM.color = Color.red;
            }
            else
            {
                Collider2D hitColliders = Physics2D.OverlapCircle(EmptyBase.transform.position, 2.2f, layerMask3); 
                if (hitColliders)
                {
                    EmptyBaseM.color = Color.yellow;
                }
                else
                {
                    RaycastHit2D hit = Physics2D.Raycast(target, (Vector2)BBase.transform.position - target,20,layerMask3);
                    if(hit.collider.tag == "Base")
                    {
                        EmptyBaseM.color = Color.green;
                        if (Input.GetMouseButtonDown(0))
                        {
                            if( Global.RedBase >= CostRed && Global.YellowBase >= CostYellow && Global.BlueBase >= CostBlue)
                            {
                                GameObject base4 = Instantiate(Resources.Load<GameObject>("Building"), target, Quaternion.identity);
                                base4.name = ((BuildingName)check).ToString();
                                BuildingSelection(base4, BBase.gameObject);
                                check = (int) BuildingName.Wait;
                            }
                        }
                    }
                    else
                    {
                        _nameBuilders.Add(BBase.name);
                        Debug.DrawRay(target, (Vector2)BBase.transform.position - target,Color.green,0.5f);
                        EmptyBaseM.color = Color.yellow;
                    }
                }
            }  
        }
        else
        {
//Включение курсора обратно
            transform.position = target;
            EmptyBase.transform.position = new Vector3 (1000, 1000, -2);
        }
        if (Input.GetMouseButtonDown(1))
        {
//Отмена постройки
            check = (int) BuildingName.Wait;
            CostRed = 0;
            CostYellow = 0;
            CostBlue = 0;
            _nameBuilders.Clear();
        }
        _distans =  Vector3.Distance(_oldVector2, target);
        if(_distans > 0.5f)
        {
            _nameBuilders.Clear();
            _oldVector2 = target;
        }
    }
    public void BuildCharging()
    {
// Включение с кнопки интерфейса
        if (check == (int) BuildingName.Wait)
        {
            check = (int) BuildingName.Base;
            CostRed = 200;
            CostYellow = 20;
        }
    }
    public void BuildFactory()
    {
// Включение с кнопки интерфейса
        if (check == (int) BuildingName.Wait)
        {
            check = (int) BuildingName.Factory;
            CostRed = 200;
            CostYellow = 20;
        }
    }
    public void BuildMagenta()
    {
// Включение с кнопки интерфейса
        if (check == (int) BuildingName.Wait)
        {
            check = (int) BuildingName.Magenta;
            CostRed = 200;
            CostYellow = 20;
            CostBlue = 5;
        }
    }
    public static void BuildingSelection(GameObject Building, GameObject NearestBuilding)
    {
        Building.GetComponent<Delivery>().AllNearBase.Add (NearestBuilding);
        Building.GetComponent<Building>().NearBase = NearestBuilding;
        if(Building.name == "Base")
        {
            Building.tag = "Base";
            Building.name = "Base" + Global.NumeBase;
            Global.NumeBase ++;
            Building.AddComponent<Base>();
            Global.RedBase -= 200;
            Global.YellowBase -= 20;
        }
        if(Building.name == "Factory")
        {
            Building.tag = "Factory";
            Building.AddComponent<Factory>();
            Global.RedBase -= 200;
            Global.YellowBase -= 20;
        }
        if(Building.name == "Magenta")
        {
            Building.tag = "Magenta";
            Building.AddComponent<Magenta>();
            Global.RedBase -= 200;
            Global.YellowBase -= 20;
            Global.BlueBase -= 5;
        }
    }
    void SearchBase()
    {
//Поиск ближайшей базы
        // Collider2D[] hitColliders = Global.Buildings.GetComponentsInChildren<Collider2D>();

        float distance = Mathf.Infinity;
        Vector3 position = EmptyBase.transform.position;
        foreach (GameObject go in Global.BuildingsList)
        {
            int _check = 0;
            foreach (var _name in _nameBuilders)
            {
                if (go.name == _name)
                {
                    _check = 1;
                    break;
                }
            }
            if(go.tag == "Base" && _check == 0)
            {
                float curDistance = Vector3.Distance(go.transform.position, position);
                if (curDistance < distance)
                {
                    BBase = go;
                    distance = curDistance;
                }
            }
        }
    }
}