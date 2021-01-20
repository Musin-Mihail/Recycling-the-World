using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static GameObject BGCave;
    public static int CheckBase = 0;
    public static int CheckFactory = 0;
    public static int CheckMagenta = 0;
    public GameObject RecyclingBlue2;
    public GameObject RecyclingMageta2;
    public static int Red = 5;
    public static int Yellow;
    public static int BaseMagenta;
    public static int RedBase = 20000;
    public static int YellowBase = 2000;
    public static int BlueBase = 200;
    public static int RandomCave;
    public static GameObject Resource;
    public static GameObject Resource2;
    public static GameObject ResourceBuild;
    public static GameObject ResourceRecycling;
    public static GameObject Buildings;
    public static GameObject Chank;
    public static GameObject Chank2;
    public static int EnergyCount = 5000;
    public static int Energy = 5;
    public static int storage = 2; 
    public static int storageCount;
    public static GameObject Earth;
    public static GameObject Cave;
    public static GameObject Enemy;
    public static GameObject EnemyStor;
    public static GameObject BG;
    public GameObject LightPlayer;
    public GameObject LightPlayerF;
    public static int NumeBase = 0;
    public static List<GameObject> ListFactory = new List<GameObject>();
    public static List<GameObject> ListMagenta = new List<GameObject>();
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
        if (CheckFactory==2)
        {
            RecyclingBlue2.SetActive(true);
        }
        else
        {
            RecyclingBlue2.SetActive(false);
        }
        if (CheckMagenta==2)
        {
            RecyclingMageta2.SetActive(true);
        }
        else
        {
            RecyclingMageta2.SetActive(false);
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
            foreach (GameObject Factory2 in ListFactory)
            {
                if(Factory2.GetComponent<Factory>().Busy == 0 )
                {
                    RedBase -=200;
                    YellowBase -=20;
                    StartCoroutine(Factory2.GetComponent<Factory>().Recycling());
                    break;
                }
            }
        }
    }
        public void RecyclingMageta()
    {
        if(RedBase>=200 && YellowBase>=20 && BlueBase>=5)
        {
            foreach (GameObject Magenta2 in ListMagenta)
            {
                if(Magenta2.GetComponent<Magenta>().Busy == 0 )
                {
                    RedBase -=200;
                    YellowBase -=20;
                    BlueBase -=5;
                    StartCoroutine(Magenta2.GetComponent<Magenta>().Recycling());
                    break;
                }
            }
        }
    }
}