using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldGeneration : MonoBehaviour
{
    float size = 10.0f;
    float DistancePlayer;
    public float ChanceYellow;
    public float ChanceRed;
    public float ChanceRes3;
    float chance;
    float DistantGen = 400;
    bool DisableColor = true;
    GameObject block;
    public static List<Vector3> newChank = new List<Vector3>();
    public static List<Vector3> vectorChank = new List<Vector3>();
    void Start()
    {
        WorldGeneration.newChank.Add(new Vector3(0,10,0));
        WorldGeneration.vectorChank.Add(new Vector3(0,10,0));
        StartCoroutine(CheckNewChank());
    }
    IEnumerator CheckNewChank()
    {
        for (int i = 0; i < 35; i++)
        {
            yield return new WaitForSeconds(0.001f);
            if(newChank.Count != 0)
            {
                ChankGeneration(newChank[0]);
                newChank.RemoveAt(0);
            }
        }
        while(true)
        {
            yield return new WaitForSeconds(0.001f);
            if(newChank.Count != 0)
            {
                ChankGeneration(newChank[0]);
                newChank.RemoveAt(0);
            }
        }
    }
    void ChankGeneration(Vector3 vector)
    {
        DistancePlayer = Vector3.Distance(vector, Vector3.zero);
        if (DistancePlayer < DistantGen)
        {
            var vectorUp = vector + Vector3.up * size;
            if (vectorUp.y <= 0)
            {
                BlockGeneration(vectorUp);
                BlockGeneration(vector - Vector3.right * size);
                BlockGeneration(vector + Vector3.right * size);
                BlockGeneration(vector - Vector3.up * size);
            }
            else
            {
                WorldGeneration.newChank.Add(vector - Vector3.up * size);
            }
        }
    }
    void BlockGeneration(Vector3 target)
    {
        bool CheckChank = true;
        foreach (var vector in vectorChank)
        {
            if(vector == target)
            {
                CheckChank = false;
                break;
            }
        }
        if (CheckChank)
        {
            WorldGeneration.newChank.Add(target);
            WorldGeneration.vectorChank.Add(target);
            var Deep = -target.y;
            var DistSpawn1 = 50;
            var DistSpawn2 = 150;
            var DistSpawn3 = 250;
            if (Deep < DistSpawn3)
            {
                ChanceRes3 = (Deep - (DistSpawn3 - 100)) / 20;
            }
            else
            {
                ChanceRes3 -= (Deep - DistSpawn3 - 100) / 20;
            }

            if (Deep < DistSpawn2)
            {
                ChanceYellow = (Deep - (DistSpawn2 - 100)) / 20;
            }
            else
            {
                ChanceYellow -= (Deep - DistSpawn2 - 100) / 20;
            }

            if (Deep < DistSpawn1)
            {
                ChanceRed = (Deep - (DistSpawn1 - 100)) / 20;
            }
            else
            {
                ChanceRed -= (Deep - DistSpawn1 - 100) / 20;
            }
            var chank = Instantiate(Global.Chank, target, Quaternion.identity, Global.Earth.transform);
            block = chank.transform.GetChild(2).gameObject;
            chance = Random.Range(0.0f, 10.0f);
            if (chance < ChanceRed)
            {
                block.tag = "Red";
                chank.name = "ChankRed";
            }
            if (chance < ChanceYellow)
            {
                block.tag = "Yellow";
                chank.name = "ChankYellow";
            }
            if (chance < ChanceRes3)
            {
                block.tag = "Res3";
                chank.name = "ChankRes3";
            }
            else
            {
                chank.name = "ChankGround";
            }
            if (DisableColor == false)
            {
                PaintBlock(chank);
            }
        }
    }
    void PaintBlock(GameObject test)
    {
        var blockColor = block.GetComponent<SpriteRenderer>();
        if (block.tag == "Red")
        {
            blockColor.color = Color.red;
        }
        else if (block.tag == "Yellow")
        {
            blockColor.color = Color.yellow;
        }
        else if (block.tag == "Res3")
        {
            blockColor.color = Color.blue;
        }
    }
}