using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayer : MonoBehaviour
{
    public static int CheckUpdate = 0;
    public static float Laser = 2;
    public static int LaserCostBlue = 1;
    public static float LaserMax = 12;
    public static int EnergyCountMax = 5000;
    public static int Energy = 5;
    public static int EnergyMax = 15;
    public static int EnergyCostBlue = 1;
    public static int storage = 2 ; 
    public static int storageMax = 12;
    public static int storageCountMax = 200;
    public static int storageCostBlue = 1;
    public GameObject Point;
    public GameObject LaserParent;
    public GameObject EnergyParent; 
    public GameObject StorageParent; 
    public GameObject Vacuum;
    public GameObject LightPlayer;
    public GameObject LightPlayerF;
    public void LaserUpdate()
    {
        if(Laser<LaserMax && Global.Blue>=LaserCostBlue)
        {
            Laser ++;
            Global.Blue -= LaserCostBlue;
            float NewPoint = -138.0f+30.2f*(Laser-3);
            GameObject point = Instantiate(Point,transform.position, Quaternion.identity);
            point.transform.parent = LaserParent.transform;
            point.transform.localPosition = new Vector3 (NewPoint,-38,0);
            point.transform.localScale = new Vector3 (42.0f,42.0f,0);
            point.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }
    public void StorageUpdate()
    {
        if(storage<storageMax && Global.Blue>=storageCostBlue)
        {
            storage ++;
            storageCountMax = storage*100;
            Global.Blue -= storageCostBlue;
            float NewPoint = -138.0f+30.2f*(storage-3);
            GameObject point = Instantiate(Point,transform.position, Quaternion.identity);
            point.transform.parent = StorageParent.transform;
            point.transform.localPosition = new Vector3 (NewPoint,38,0);
            point.transform.localScale = new Vector3 (42.0f,42.0f,0);
            point.GetComponent<SpriteRenderer>().sortingOrder = 2;
            Vacuum.GetComponent<CircleCollider2D>().radius = 4+ (float)storage/2;
            LightPlayer.GetComponent<Light>().range +=0.5f;
            LightPlayer.GetComponent<Light>().spotAngle +=10;
            LightPlayerF.GetComponent<Light>().range +=10;
            LightPlayerF.GetComponent<Light>().spotAngle +=2;
        }
    }
    public void EnergyUpdate()
    {
        if(Energy<EnergyMax && Global.Blue>=EnergyCostBlue)
        {
            Energy ++;
            EnergyCountMax = Energy*1000;
            Global.Blue -= EnergyCostBlue;
            float NewPoint = -138.0f+30.2f*(Energy-6);
            GameObject point = Instantiate(Point,transform.position, Quaternion.identity);
            point.transform.parent = EnergyParent.transform;
            point.transform.localPosition = new Vector3 (NewPoint,0,0);
            point.transform.localScale = new Vector3 (42.0f,42.0f,0);
            point.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }
}