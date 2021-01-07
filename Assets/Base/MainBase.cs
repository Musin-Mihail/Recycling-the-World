using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBase : MonoBehaviour
{
    void Start()
    {
        Global.MainBase = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Red2")
        {
            Global.RedBase ++;
            Destroy(other.gameObject);
        }
        else if(other.tag == "Yellow2")
        {
            Global.YellowBase ++;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (Global.EnergyCount<UpdatePlayer.EnergyCountMax)
            {
                Global.EnergyCount +=50;
            }
            if(Global.Red > 0)
            {
                Global.Red --;
                Global.RedBase ++;
            }
            if(Global.Yellow > 0)
            {
                Global.Yellow --;
                Global.YellowBase ++;
            }
        }
    }
}
