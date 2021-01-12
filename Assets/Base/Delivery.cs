using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public List<GameObject> AllNearBase = new List<GameObject>();
    public void Search(List<GameObject> test, GameObject deleted, string name)
    {
        foreach (var Nbase in AllNearBase)
        {
            if (Nbase.name == name)
            {
                if (name == "Factory")
                {
                    if(Nbase.GetComponent<Factory>().Busy == 0)
                    {
                        test.Add (Nbase);
                        test.Add (gameObject);
                        Nbase.GetComponent<Factory>().Busy = 1;
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    test.Add (Nbase);
                    test.Add (gameObject);
                    return;
                }
            }          
        }
        foreach (var Nbase in AllNearBase)
        {
            if (Nbase.tag == "Base")
            {
                if (Nbase.name != deleted.name)
                {
                    Nbase.GetComponent<Delivery>().Search(test, gameObject, name);
                    if (test.Count > 0 && test[0].name == name)
                    {
                        test.Add (gameObject);
                        break;
                    }
                }  
            }
        }
    }
}