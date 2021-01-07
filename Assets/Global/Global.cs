using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static GameObject BGCave;
    public static int CheckBase = 0;
    public static int CheckFactory = 0;
    public GameObject Recycling;
    public GameObject DisableFactory;
    public static int Red;
    public static int Yellow;
    public static int Blue;
    public static int RedBase=200;
    public static int YellowBase=20;
    public static int RandomCave;
    public static GameObject Resource;
    public static GameObject Resource2;
    public static GameObject ResourceBuild;
    public static GameObject ResourceRecycling;
    public static GameObject Buildings;
    public static Vector3 MainBase;
    public static GameObject Chank;
    public static GameObject Chank2;
    public static int EnergyCount = 5000;
    public static int Energy = 5;
    public static int storage = 2; 
    public static int storageCount;
    public static GameObject Earth;
    public static GameObject Cave;
    public static GameObject Enemy;
    public static GameObject Factory;
    public static GameObject EnemyStor;
    public static GameObject BG;
    public GameObject LightPlayer;
    public GameObject LightPlayerF;
    void Awake()
    {
        BG = Resources.Load<GameObject>("BG");
        ResourceRecycling = Resources.Load<GameObject>("ResourceRecycling");
        ResourceBuild = Resources.Load<GameObject>("ResourceBuild");
        BGCave = Resources.Load<GameObject>("BGCave");
        Enemy = Resources.Load<GameObject>("Enemy");
        Earth = GameObject.Find("Earth");
        EnemyStor = GameObject.Find("EnemyStor");
        RandomCave = Random.Range(500,1500);
        Resource = Resources.Load<GameObject>("Resource");
        Resource2 = Resources.Load<GameObject>("Resource3");
        Buildings = GameObject.Find("Buildings");
        Chank2 = Resources.Load<GameObject>("Square");
        Chank = Resources.Load<GameObject>("Chank");
        Cave = Resources.Load<GameObject>("Cave");
    }
    void Update()
    {
        if(CheckFactory==1)
        {
            DisableFactory.SetActive(false);
        }
        else if (CheckFactory==2)
        {
            Recycling.SetActive(true);
        }
        else
        {
            Recycling.SetActive(false);
        }
        storageCount = Red+Yellow;
        if(EnergyCount < 0)
        {
            EnergyCount = 0;
        }
        if(EnergyCount > UpdatePlayer.EnergyCountMax)
        {
            EnergyCount = UpdatePlayer.EnergyCountMax;
        }
        if(EnergyCount<500)
        {
            LightPlayer.GetComponent<Light>().intensity = Random.Range(0.0f,1.0f);
            LightPlayerF.GetComponent<Light>().intensity = Random.Range(0.0f,1.0f);
        }
        else if(EnergyCount<1000)
        {
            LightPlayer.GetComponent<Light>().intensity = 1;
            LightPlayerF.GetComponent<Light>().intensity = 1;
        }
        else if(EnergyCount<2000)
        {
            LightPlayer.GetComponent<Light>().intensity = 2;
            LightPlayerF.GetComponent<Light>().intensity = 2;
        }
        else
        {
            LightPlayer.GetComponent<Light>().intensity = 3;
            LightPlayerF.GetComponent<Light>().intensity = 5;
        }        
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void RecyclingBlue()
    {
        if(RedBase>=200 && YellowBase>=20)
        {
            CheckFactory = 0;
            RedBase -=200;
            YellowBase -=20;
            StartCoroutine(Factory.GetComponent<Factory>().Recycling());
        }
    }
}