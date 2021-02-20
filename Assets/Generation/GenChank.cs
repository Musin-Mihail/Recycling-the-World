using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenChank : MonoBehaviour
{
//Создание чанков
    float size = 10.0f;
    float DistancePlayer;
    Vector3 Target1;
    Vector3 Target2;
    Vector3 Target3;
    Vector3 Target4;
    int test=0;
    int layerMask = 1 << 13;
    public float ChanceYellow = 0;
    public float ChanceRed = 0;
    public float ChanceRes3 = 0;
    public float chance;
    float CorrectChance;
    int DisableColor = 1;
    float DistantGen = 50;
    void Start()
    {
        Target1 = new Vector2(transform.position.x-size,transform.position.y);
        Target2 = new Vector2(transform.position.x+size,transform.position.y);
        Target3 = new Vector2(transform.position.x,transform.position.y-size);
        Target4 = new Vector2(transform.position.x,transform.position.y+size);
//Корректировка шанса появления ресурсов от глубины
        var Deep = -gameObject.transform.position.y;
        var DistSpawn1 = 50;
        var DistSpawn2 = 150;
        var DistSpawn3 = 250;
        if(Deep < DistSpawn3)
        {
            ChanceRes3 = (Deep-(DistSpawn3-100))/20;
        }
        else
        {
            ChanceRes3 -= (Deep - DistSpawn3-100)/20;
        }

        if(Deep < DistSpawn2)
        {
            ChanceYellow = (Deep-(DistSpawn2-100))/20;
        }
        else
        {
            ChanceYellow -= (Deep - DistSpawn2-100)/20;
        }

        if(Deep < DistSpawn1)
        {
            ChanceRed = (Deep-(DistSpawn1-100))/20;
        }
        else
        {
            ChanceRed -= (Deep - DistSpawn1 - 100)/20;
        }
    }
    void Update()
    {
        DistancePlayer = Vector3.Distance(transform.position,PlayerGlobal.Player.transform.position);
        if (DistancePlayer<DistantGen && test==0)
        {
            test = 1;
            GenChank2();
        }
    }
    void GenChank2()
    {
        GenChank3(Target1);
        GenChank3(Target2);
        GenChank3(Target3);
        if(Target4.y <=0)
        {
            GenChank3(Target4);
        }
        Destroy(gameObject.GetComponent<GenChank>());
    }
    void GenChank3(Vector3 target)
    {
        Collider2D CheckChank = Physics2D.OverlapCircle(target, 1, layerMask);
        if(CheckChank==null)
        {
            var block = Instantiate(Global.Chank, target, transform.rotation);
            block.name = "Chank";
            chance = Random.Range(0.0f,10.0f);
            
            if(chance<ChanceRed)
            {
                block.GetComponent<Chank>().Child.tag = "Red";
                ColorChank(block);
            }
            if(chance<ChanceYellow)
            {
                block.GetComponent<Chank>().Child.tag = "Yellow";
                ColorChank(block);
            }
            if(chance<ChanceRes3)
            {
                block.GetComponent<Chank>().Child.tag = "Res3";
                ColorChank(block);
            }
            block.transform.parent = Global.Earth.transform;
        }
    }
    void ColorChank(GameObject test)
    {
        if(DisableColor == 0)
        {
            if(test.GetComponent<Chank>().Child.tag == "Red")
            {
                test.GetComponent<Chank>().Child.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if(test.GetComponent<Chank>().Child.tag == "Yellow")
            {
                test.GetComponent<Chank>().Child.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if(test.GetComponent<Chank>().Child.tag == "Res3")
            {
                test.GetComponent<Chank>().Child.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
    }
}