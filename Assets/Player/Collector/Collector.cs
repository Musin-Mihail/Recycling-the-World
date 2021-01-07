using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{ 
//Сбор ресурсов
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.name == "Resource")
        {
            Global.storageCount = Global.Red+Global.Yellow;
            if(Global.storageCount < UpdatePlayer.storageCountMax)
            {
                if(other.tag == "Yellow")
                {
                    Destroy(other.gameObject);
                    Global.Yellow ++;
                    Global.EnergyCount --;
                }
                else if(other.tag == "Red")
                {
                    Destroy(other.gameObject);
                    Global.Red ++;
                    Global.EnergyCount --;
                }
                else if(other.tag == "Blue")
                {
                    Destroy(other.gameObject);
                    Global.Blue ++;
                    Global.EnergyCount --;
                }
            }
        }
    }
}