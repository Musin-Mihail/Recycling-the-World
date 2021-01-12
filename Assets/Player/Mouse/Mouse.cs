using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
// Курсор и установка зданий
    public int check = 0;
    public GameObject Base2;
    public GameObject Factory;
    public GameObject EmptyBase;
    public Collider2D BBase;
    public float BaseDistance;
    public SpriteRenderer EmptyBaseM;
    int layerMask = 1 << 8;//Блоки
    int layerMask2 = 1 << 9;//База
    int layerMask3;
    public Vector2 target;
    void Start()
    {
//Объединение слоёв
        layerMask3 = layerMask | layerMask2;
    }
    void Update()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (check==1)
        {
//Если выбрано здание
            EmptyBase.transform.position = target;
            SearchBase();
            transform.position = new Vector3 (1000, 1000, -3);
            BaseDistance = Vector3.Distance(BBase.transform.position, EmptyBase.transform.position);
            if(BaseDistance>20 || Global.RedBase < 200 || Global.YellowBase < 20)
            {
//Если далеко или нет ресурсов
                EmptyBaseM.color = Color.red; 
            }
            else
            {
//Если ли место постройки занято
                Collider2D hitColliders = Physics2D.OverlapCircle(EmptyBase.transform.position, 2.2f, layerMask3); 
                if (hitColliders)
                {
                    EmptyBaseM.color = Color.yellow;
                }
                else
                {
// Если ли между новой и старой базой есть препятствие
                    RaycastHit2D hit = Physics2D.Raycast(target, (Vector2)BBase.transform.position - target,20,layerMask3);
                    if(hit.collider.tag == "Base")
                    {
                        EmptyBaseM.color = Color.green;
                        if (Input.GetMouseButtonDown(0))
                        {
                            if(Global.RedBase >= 200 && Global.YellowBase >= 20)
                            {
//Постройка здания
                                GameObject base4 = Instantiate(Base2, target, Quaternion.identity);
                                // base4.transform.parent = Global.Buildings.transform;
                                base4.GetComponent<Base>().NearBase = BBase.gameObject;
                                base4.name = "Base"+ Global.NumeBase;
                                Global.NumeBase ++;
                                Global.RedBase -= 200;
                                Global.YellowBase -= 20;
                                check = 0;
                                Global.CheckBase = 0;
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
        else if (check==2)
        {
//Если выбрано здание
            EmptyBase.transform.position = target;
            SearchBase();
            transform.position = new Vector3 (1000, 1000, -3);
            BaseDistance = Vector3.Distance(BBase.transform.position, EmptyBase.transform.position);
            if(BaseDistance>20 || Global.RedBase < 200 || Global.YellowBase < 20)
            {
//Если далеко или нет ресурсов
                EmptyBaseM.color = Color.red; 
            }
            else
            {
//Если ли место постройки занято
                Collider2D hitColliders = Physics2D.OverlapCircle(EmptyBase.transform.position, 2.2f, layerMask3); 
                if (hitColliders)
                {
                    EmptyBaseM.color = Color.yellow;
                }
                else
                {
// Если ли между новой и старой базой есть препятствие
                    RaycastHit2D hit = Physics2D.Raycast(target, (Vector2)BBase.transform.position - target,20,layerMask3);
                    if(hit.collider.tag == "Base")
                    {
                        EmptyBaseM.color = Color.green;
                        if (Input.GetMouseButtonDown(0))
                        {
                            if(Global.RedBase >= 200 && Global.YellowBase >= 20)
                            {
//Постройка здания
                                GameObject base4 = Instantiate(Factory, target, Quaternion.identity);
                                // base4.transform.parent = Global.Buildings.transform;
                                base4.GetComponent<Factory>().NearBase = BBase.gameObject;
                                base4.name = "Factory";
                                Global.RedBase -= 200;
                                Global.YellowBase -= 20;
                                check = 0;
                                Global.CheckBase = 0;
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
                check = 0;
                Global.CheckBase = 0;
            }
    }
    public void BuildCharging()
    {
// Включение с кнопки интерфейса
        if (check == 0)
        {
            check = 1;
            Global.CheckBase = 1;
        }
    }
    public void BuildFactory()
    {
// Включение с кнопки интерфейса
        if (check == 0)
        {
            check = 2;
            Global.CheckBase = 1;
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