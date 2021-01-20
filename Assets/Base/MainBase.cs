using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBase : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (Global.EnergyCount<UpdatePlayer.EnergyCountMax)
            {
                Global.EnergyCount +=50;
            }
            for(int i = 0; i < Global.storage; i++)
            {
                if(Global.Red > 0)
                {
                    Global.Red --;
                    Global.RedBase ++;
                }
                else if(Global.Yellow > 0)
                {
                    Global.Yellow --;
                    Global.YellowBase ++;
                }
            }
        }
    }
}