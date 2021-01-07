using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{
//Генерация маркеров для создания пещер
    float size;
    float sizeBlock = 0.6f; //1.2f, 0.6f, 0.3f
    Vector3 Target1;
    Vector3 Target2;
    Vector3 Target3;
    Vector3 Target4;
    float PerlinNoiseCave;
    private float noiseScale = 0.06f;
    private float threshold = 0.2f;
    public GameObject Parent;
    void Start()
    {
        size = transform.localScale.x;
        Target1 = new Vector3(transform.position.x-size/4,transform.position.y-size/4,transform.position.z);
        Target2 = new Vector3(transform.position.x-size/4,transform.position.y+size/4,transform.position.z);
        Target3 = new Vector3(transform.position.x+size/4,transform.position.y-size/4,transform.position.z);
        Target4 = new Vector3(transform.position.x+size/4,transform.position.y+size/4,transform.position.z);
        Invoke("DestroyCave",6);
        GenCave();
    }
    void GenCave()
    {
//Генерация пещер
        PerlinNoiseCave = Mathf.PerlinNoise((transform.position.x+Global.RandomCave)*noiseScale, (transform.position.y+Global.RandomCave)*noiseScale);
        float fff = threshold+transform.localScale.x/10;
        if(PerlinNoiseCave>fff)
        {
            Destroy(gameObject);
        }
        else
        {
            if(transform.localScale.x >= sizeBlock)
            {
                Destroy(gameObject);
                GenChank(Target1);
                GenChank(Target2);
                GenChank(Target3);
                GenChank(Target4);
            }
        }
    }
    void GenChank(Vector3 target)
    {
//Деление блоков
        var block2 = Instantiate(Global.Cave, target, transform.rotation);
        block2.transform.localScale = new Vector3(size/2,size/2,1);
        block2.name = "Cave";
        block2.transform.parent = Parent.transform;
        block2.GetComponent<Cave>().Parent = Parent;
    }
    void DestroyCave()
    {
        Destroy(gameObject);
    }
}
