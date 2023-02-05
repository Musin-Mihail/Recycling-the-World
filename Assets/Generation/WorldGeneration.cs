using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    float noise1 = 0.55f;
    float noise2 = 0.60f;
    float noise3 = 0.70f;
    float noise4 = 0.75f;
    float noise5 = 0.785f;

    float noise6 = 0.796f;

    // float ChanceYellow;
    // float ChanceRed;
    // float ChanceRes3;
    // float chance;
    // bool DisableColor = true;
    List<Vector3> NewBasicChank = new List<Vector3>();
    List<Vector3> AllBasicVector3Chank = new List<Vector3>();
    List<Vector3> AllVectorBlock = new List<Vector3>();
    public List<Transform> caveVector = new List<Transform>();
    float noiseScale = 0.02f;
    public GameObject Chank1;
    public GameObject Chank2;
    public GameObject Chank3;
    public GameObject Chank4;
    public GameObject Chank5;
    public GameObject Chank6;
    Vector3 V1 = new Vector3(1, 1, 0);
    Vector3 V2 = new Vector3(-1, -1, 0);
    Vector3 V3 = new Vector3(-1, 1, 0);
    Vector3 V4 = new Vector3(1, -1, 0);
    int maxDistans = 1000;
    public GameObject BlackChank;

    void Start()
    {
        StartCoroutine(BasicChankGeneration());
    }

    IEnumerator BasicChankGeneration()
    {
        NewBasicChank.Add(new Vector3(0, 0, 0));
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (NewBasicChank.Count != 0)
            {
                if (Vector3.Distance(NewBasicChank[0], Vector3.zero) < maxDistans)
                {
                    Vector3 vectorUp = NewBasicChank[0] + Vector3.up * 10;
                    if (vectorUp.y <= 0)
                    {
                        AddVector3Chank(NewBasicChank[0] + Vector3.up * 10);
                    }

                    AddVector3Chank(NewBasicChank[0] - Vector3.right * 10);
                    AddVector3Chank(NewBasicChank[0] + Vector3.right * 10);
                    AddVector3Chank(NewBasicChank[0] - Vector3.up * 10);
                    bool CheckChank = true;
                    foreach (var vector in AllVectorBlock)
                    {
                        if (NewBasicChank[0] == vector)
                        {
                            CheckChank = false;
                            break;
                        }
                    }

                    if (CheckChank)
                    {
                        if (PerlinNoise(NewBasicChank[0]) < noise1)
                        {
                            GameObject chank = Instantiate(Chank1, NewBasicChank[0], Quaternion.identity, Global.Earth.transform);
                            chank.name = "BasicChank";
                        }
                        else
                        {
                            GameObject chank = Instantiate(BlackChank, NewBasicChank[0], Quaternion.identity, Global.Earth.transform);
                            // GameObject chank =  new GameObject("chank");
                            chank.transform.parent = Global.Earth.transform;
                            CreateCave(NewBasicChank[0], 2.5f, chank);
                        }

                        AllVectorBlock.Add(NewBasicChank[0]);
                    }
                }

                NewBasicChank.RemoveAt(0);
            }
        }
    }

    void CreateCave(Vector3 vector, float scale, GameObject parentChank)
    {
        GameObject chank = null;
        float noise = 0;
        if (scale == 2.5f)
        {
            chank = Chank2;
            noise = noise2;
        }
        else if (scale == 1.25f)
        {
            chank = Chank3;
            noise = noise3;
        }
        else if (scale == 0.625f)
        {
            chank = Chank4;
            noise = noise4;
        }
        else if (scale == 0.3125f)
        {
            chank = Chank5;
            noise = noise5;
        }
        else if (scale == 0.15625f)
        {
            chank = Chank6;
            noise = noise6;
        }

        if (chank != null)
        {
            Vector3 vector1 = vector + V1 * scale;
            Vector3 vector2 = vector + V2 * scale;
            Vector3 vector3 = vector + V3 * scale;
            Vector3 vector4 = vector + V4 * scale;
            if (PerlinNoise(vector1) < noise)
            {
                GameObject block1 = Instantiate(chank, vector1, Quaternion.identity, parentChank.transform);
            }
            else
            {
                CreateCave(vector1, scale / 2, parentChank);
            }

            if (PerlinNoise(vector2) < noise)
            {
                GameObject block2 = Instantiate(chank, vector2, Quaternion.identity, parentChank.transform);
            }
            else
            {
                CreateCave(vector2, scale / 2, parentChank);
            }

            if (PerlinNoise(vector3) < noise)
            {
                GameObject block3 = Instantiate(chank, vector3, Quaternion.identity, parentChank.transform);
            }
            else
            {
                CreateCave(vector3, scale / 2, parentChank);
            }

            if (PerlinNoise(vector4) < noise)
            {
                GameObject block4 = Instantiate(chank, vector4, Quaternion.identity, parentChank.transform);
            }
            else
            {
                CreateCave(vector4, scale / 2, parentChank);
            }
        }
    }

    void AddVector3Chank(Vector3 vector1)
    {
        bool CheckChank = true;
        foreach (var vector2 in AllBasicVector3Chank)
        {
            if (vector1 == vector2)
            {
                CheckChank = false;
                break;
            }
        }

        if (CheckChank)
        {
            NewBasicChank.Add(vector1);
            AllBasicVector3Chank.Add(vector1);
        }
    }

    float PerlinNoise(Vector3 vector)
    {
        float value = Mathf.PerlinNoise((vector.x + Global.RandomCave) * noiseScale, (vector.y + Global.RandomCave) * noiseScale);
        foreach (var vectorCave in caveVector)
        {
            float scale = vectorCave.localScale.x;
            float distans = Vector3.Distance(vectorCave.position, vector);
            float value2 = 1 - ((distans / (scale / 40)) / 100);
            if (value2 > value)
            {
                value = value2;
            }
        }

        return value;
    }
    //             var Deep = -target.y;
    //             var DistSpawn1 = 50;
    //             var DistSpawn2 = 150;
    //             var DistSpawn3 = 250;
    //             if (Deep < DistSpawn3)
    //             {
    //                 ChanceRes3 = (Deep - (DistSpawn3 - 100)) / 20;
    //             }
    //             else
    //             {
    //                 ChanceRes3 -= (Deep - DistSpawn3 - 100) / 20;
    //             }
    //             if (Deep < DistSpawn2)
    //             {
    //                 ChanceYellow = (Deep - (DistSpawn2 - 100)) / 20;
    //             }
    //             else
    //             {
    //                 ChanceYellow -= (Deep - DistSpawn2 - 100) / 20;
    //             }
    //             if (Deep < DistSpawn1)
    //             {
    //                 ChanceRed = (Deep - (DistSpawn1 - 100)) / 20;
    //             }
    //             else
    //             {
    //                 ChanceRed -= (Deep - DistSpawn1 - 100) / 20;
    //             }
    //             chance = UnityEngine.Random.Range(0.0f, 10.0f);
    //             if (chance < ChanceRed)
    //             {
    //                 chank.tag = "Red";
    //                 chank.name = "ChankRed";
    //             }
    //             if (chance < ChanceYellow)
    //             {
    //                 chank.tag = "Yellow";
    //                 chank.name = "ChankYellow";
    //             }
    //             if (chance < ChanceRes3)
    //             {
    //                 chank.tag = "Res3";
    //                 chank.name = "ChankRes3";
    //             }
    //             else
    //             {
    //                 chank.name = "ChankGround";
    //             }
    //             if (DisableColor == false)
    //             {
    //                 PaintBlock(chank);
    //             }
    //             if(scale.x == 10)
    //             {
    //                 CreateVector2(target,scale);
    //             }
    //             else if(scale.x == 5f)
    //             {
    //                 CreateVector(target,scale);
    //             }
    //         }
    //     }
    // }
    // void PaintBlock(GameObject test)
    // {
    //     var blockColor = block.GetComponent<SpriteRenderer>();
    //     if (block.tag == "Red")
    //     {
    //         blockColor.color = Color.red;
    //     }
    //     else if (block.tag == "Yellow")
    //     {
    //         blockColor.color = Color.yellow;
    //     }
    //     else if (block.tag == "Res3")
    //     {
    //         blockColor.color = Color.blue;
    //     }
    // }

    //     float chance = Random.Range(0.0f,10.0f);
    //     if(block2.transform.localScale.x <= 1.0f)
    //     {
    //         if(chance<8.0f)
    //         {
    //             block2.tag = "Yellow";
    //             ColorChank(block2);
    //         }
    //         if(chance<1.0f)
    //         {
    //             block2.tag = "Red";
    //             ColorChank(block2);
    //         }
    //     }
    //     else
    //     {
    //         if(transform.tag == "Red")
    //         {
    //             if(chance<6.0f)
    //             {
    //                 block2.tag = "Red";
    //                 ColorChank(block2);
    //             }
    //             if(chance<0.1f)
    //             {
    //                 block2.tag = "Yellow";
    //                 ColorChank(block2);
    //             }
    //         }
    //         else if (transform.tag == "Yellow")
    //         {
    //             if(chance<4.0f)
    //             {
    //                 block2.tag = "Yellow";
    //                 ColorChank(block2);
    //             }
    //             if (chance<0.1f)
    //             {
    //                 block2.tag = "Red";
    //                 ColorChank(block2);
    //             }
    //         }
    //     }
    // }

    //     if(DisableColor == 0)
    //     {
    //         if(test.tag == "Red")
    //         {
    //             test.GetComponent<SpriteRenderer>().color = Color.red;
    //         }
    //         if(test.tag == "Yellow")
    //         {
    //             test.GetComponent<SpriteRenderer>().color = Color.yellow;
    //         }
    //     }
    // }

    //     chance = Random.Range(0.0f,10.0f);
    //     if(transform.tag == "Red")
    //     {
    //         if(chance<6.0f)
    //         {
    //             block2.tag = "Red";
    //             ColorChank(block2);
    //         }
    //         if(chance<0.1f)
    //         {
    //             block2.tag = "Yellow";
    //             ColorChank(block2);
    //         }
    //     }
    //     else if (transform.tag == "Yellow")
    //     {
    //         if(chance<4.0f)
    //         {
    //             block2.tag = "Yellow";
    //             ColorChank(block2);
    //         }
    //         if (chance<0.1f)
    //         {
    //             block2.tag = "Red";
    //             ColorChank(block2);
    //         }
    //     }
    // }
}