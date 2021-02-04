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
    public Collider2D BBase;
    public float BaseDistance;
    public SpriteRenderer EmptyBaseM;
    int layerMask = 1 << 8;//Блоки
    int layerMask2 = 1 << 9;//База
    int layerMask3;
    public Vector2 target;
    enum Building {Wait, Base, Factory, Magenta};
    void Start()
    {
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
            if(BaseDistance > 20)
            {
                EmptyBaseM.color = Color.red;
            }
            else
            {
                if ((check == (int) Building.Base && Global.RedBase < 200 || Global.YellowBase < 20))
                {
                    EmptyBaseM.color = Color.red;
                }
                else if (check == (int) Building.Factory && Global.RedBase < 200 || Global.YellowBase < 20)
                {
                    EmptyBaseM.color = Color.red;
                }
                else if (check == (int) Building.Magenta && Global.RedBase < 200 || Global.YellowBase < 20 || Global.BlueBase < 5)
                {
                    EmptyBaseM.color = Color.red;
                }
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
                            if(check == (int) Building.Base && Global.RedBase >= 200 && Global.YellowBase >= 20)
                            {
                                GameObject base4 = Instantiate(Resources.Load<GameObject>("Building"), target, Quaternion.identity);
                                base4.name = nameof(Building.Base);
                                BaseBuild.BuildingSelection(base4, BBase.gameObject);
                                check = (int) Building.Wait;
                            }
                            if(check == (int) Building.Factory && Global.RedBase >= 200 && Global.YellowBase >= 20)
                            {
                                GameObject base4 = Instantiate(Resources.Load<GameObject>("Building"), target, Quaternion.identity);
                                base4.name = nameof(Building.Factory);
                                BaseBuild.BuildingSelection(base4, BBase.gameObject);
                                check = (int) Building.Wait;
                            }
                            if(check == (int) Building.Magenta && Global.RedBase >= 200 && Global.YellowBase >= 20 && Global.BlueBase >= 5)
                            {
                                GameObject base4 = Instantiate(Resources.Load<GameObject>("Building"), target, Quaternion.identity);
                                base4.name = nameof(Building.Magenta);
                                BaseBuild.BuildingSelection(base4, BBase.gameObject);
                                check = (int) Building.Wait;
                            }
                        }
                    }
                    else
                    {
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
                check = (int) Building.Wait;
                // Global.CheckBase = 0;
            }
    }
    public void BuildCharging()
    {
// Включение с кнопки интерфейса
        if (check == (int) Building.Wait)
        {
            check = (int) Building.Base;
            // Global.CheckBase = 1;
        }
    }
    public void BuildFactory()
    {
// Включение с кнопки интерфейса
        if (check == (int) Building.Wait)
        {
            check = (int) Building.Factory;
            // Global.CheckBase = 1;
        }
    }
    public void BuildMagenta()
    {
// Включение с кнопки интерфейса
        if (check == (int) Building.Wait)
        {
            check = (int) Building.Magenta;
            // Global.CheckBase = 1;
        }
    }
    void SearchBase()
    {
//Поиск ближайшей базы
        Collider2D[] hitColliders = Global.Buildings.GetComponentsInChildren<Collider2D>();
        float distance = Mathf.Infinity;
        Vector3 position = EmptyBase.transform.position;
        foreach (Collider2D go in hitColliders)
        {
            if(go.tag == "Base")
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