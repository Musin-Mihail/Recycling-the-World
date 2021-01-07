using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" )
        {
            if (Global.EnergyCount<UpdatePlayer.EnergyCountMax)
            {
                Global.EnergyCount +=50;
            }
        }
    }
}