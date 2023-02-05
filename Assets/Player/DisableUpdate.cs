using UnityEngine;

public class DisableUpdate : MonoBehaviour
{
//Отключение строительства и улучшения, когда база далеко
    GameObject BBase;
    float BaseDistance;
    public GameObject DisableDistant;


    void Update()
    {
        if (BBase == null)
        {
            SearchBase();
        }
        else
        {
            BaseDistance = Vector3.Distance(BBase.transform.position, transform.position);
//Отключение постройки и улучшения
            if (BaseDistance > 20)
            {
                SearchBase();
                DisableDistant.SetActive(false);
            }
            else
            {
                DisableDistant.SetActive(true);
            }
        }
    }

//Поиск ближайшего здания
    void SearchBase()
    {
        // Collider2D[] hitColliders = Global.Buildings.GetComponentsInChildren<Collider2D>();
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in Global.BuildingsList)
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