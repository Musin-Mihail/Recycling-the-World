using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuild : MonoBehaviour
{
    public static GameObject Earth;

    public static void StartBuild(GameObject Build, GameObject Beam, GameObject Point, GameObject NearBase)
    {
        Build.GetComponent<SpriteRenderer>().color = Color.yellow;
        float BaseDistance = Vector3.Distance(Build.transform.position, NearBase.transform.position);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Point.transform.rotation = Quaternion.LookRotation(Vector3.forward, NearBase.transform.position - Point.transform.position);
        Beam.transform.localPosition = new Vector2(0, BaseDistance / 6);
        Beam.transform.localScale = new Vector3(Beam.transform.localScale.x, BaseDistance * 0.28f, 1);
    }

    public static IEnumerator Build(List<GameObject> MainBase, int Red = 0, int Yellow = 0, int Blue = 0, int AllCost = 0)
    {
        int count = 0;
        while (Red > 0 || Yellow > 0 || Blue > 0)
        {
            count++;
            var res = Instantiate(Global.ResourceBuild, MainBase[0].transform.position, Quaternion.identity);
            res.GetComponent<ResourceBuild>().ListBase = MainBase;
            if (Red > 0)
            {
                Red--;
                res.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (Yellow > 0)
            {
                Yellow--;
                res.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (Blue > 0)
            {
                Blue--;
                res.GetComponent<SpriteRenderer>().color = Color.blue;
            }

            if (count == AllCost)
            {
                res.name = "EndRes";
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    public static IEnumerator Recycling(List<GameObject> MainBase, int Red = 0, int Yellow = 0, int Blue = 0, int AllCost = 0)
    {
        int count = 0;
        while (Red > 0 || Yellow > 0 || Blue > 0)
        {
            count++;
            var res = Instantiate(Global.ResourceRecycling, MainBase[0].transform.position, Quaternion.identity);
            res.GetComponent<ResourceRecycling>().ListBase = MainBase;
            if (Red > 0)
            {
                Red--;
                res.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (Yellow > 0)
            {
                Yellow--;
                res.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (Blue > 0)
            {
                Blue--;
                res.GetComponent<SpriteRenderer>().color = Color.blue;
            }

            if (count == AllCost)
            {
                res.name = "EndRes";
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}