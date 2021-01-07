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
    float ChanceGold = 0;
    float ChanceSilver = 0;
    float CorrectChance;
    int DisableColor = 1;
    float DistantGen = 50;
    void Start()
    {
        Target1 = new Vector2(transform.position.x-size,transform.position.y);
        Target2 = new Vector2(transform.position.x+size,transform.position.y);
        Target3 = new Vector2(transform.position.x,transform.position.y-size);
        Target4 = new Vector2(transform.position.x,transform.position.y+size);
//Корректировка шанка появления ресурсов от глубины
        CorrectChance = gameObject.transform.position.y/50;

        ChanceSilver = 16 + CorrectChance*2;
//Чем глубже тем меньше красного

//Жёлтого больше всего на глубине -200. -200/50 = -4
        if (CorrectChance>-4)
        {
            ChanceGold = 1 - CorrectChance;
        }
        else if(CorrectChance<-4)
        {
            ChanceGold = 9 + CorrectChance;
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
            float chance = Random.Range(0.0f,10.0f);
            
            if(chance<ChanceSilver)
            {
                block.GetComponent<Chank>().Child.tag = "Silver";
                ColorChank(block);
            }
            if(chance<ChanceGold)
            {
                block.GetComponent<Chank>().Child.tag = "Gold";
                ColorChank(block);
            }
            block.transform.parent = Global.Earth.transform;
        }
    }
    void ColorChank(GameObject test)
    {
        if(DisableColor == 0)
        {
            if(test.GetComponent<Chank>().Child.tag == "Silver")
            {
                test.GetComponent<Chank>().Child.GetComponent<Renderer>().material.color = Color.red;
            }
            else if(test.GetComponent<Chank>().Child.tag == "Gold")
            {
                test.GetComponent<Chank>().Child.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }
}