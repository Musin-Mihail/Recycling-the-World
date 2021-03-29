using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBase : MonoBehaviour
{
    void Start()
    {
        Global.BuildingsList.Add(gameObject);
        Global.BuildingsDiger.Add(gameObject);
        Global.BuildingsCharge.Add(gameObject);
    }
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if(other.tag == "Dron")
    //     {
    //         if(other.transform.position == transform.position)
    //         {
    //             Debug.Log(other.tag);
    //             for(int i = 0; i < Global.storage; i++)
    //             {
    //                 if(Global.Red > 0)
    //                 {
    //                     Global.Red --;
    //                     Global.RedBase ++;
    //                 }
    //                 else if(Global.Yellow > 0)
    //                 {
    //                     Global.Yellow --;
    //                     Global.YellowBase ++;
    //                 }
    //             }
    //         }
    //     }
    // }
}