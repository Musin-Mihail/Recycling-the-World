using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    int layerMask = 1 << 8;
    int layerMask2 = 1 << 17;
    int layerMask3;
    public float DistancePlayer;
    void Start()
    {
        layerMask3 = layerMask | layerMask2;
    }
//Поворот за мышкой
    void Update()
    {
//Поворот в сторону мыши
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        //Пуск луча и уничтожение заглушек.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos - transform.position,30,layerMask3);
        if(hit && hit.collider.name == "Top")
        {
            Destroy(hit.collider.gameObject);
        }
    }
}