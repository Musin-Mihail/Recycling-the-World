using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenBG : MonoBehaviour
{
//Создание чанков
    float DistancePlayer;
    Vector3 Target1;
    Vector3 Target2;
    Vector3 Target3;
    Vector3 Target4;
    int test=0;
    int layerMask = 1 << 16;
    float DistantGen = 100;
    void Start()
    {
        Target1 = new Vector2(transform.position.x-100,transform.position.y);
        Target2 = new Vector2(transform.position.x+100,transform.position.y);
        Target3 = new Vector2(transform.position.x,transform.position.y-100);
        Target4 = new Vector2(transform.position.x,transform.position.y+100);
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
        Destroy(gameObject.GetComponent<GenBG>());
    }
    void GenChank3(Vector3 target)
    {
        Collider2D CheckChank = Physics2D.OverlapCircle(target, 1, layerMask);
        if(CheckChank==null)
        {
            var block = Instantiate(Global.BG, target, transform.rotation);
            // block.name = "Chank";
        }
    }
}