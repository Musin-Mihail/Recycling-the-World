using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    int LaserCheck = 0;
    public GameObject LaserRot;
    public GameObject Laser;
    public GameObject LaserPoint;
    public GameObject Body;
    
    void Start()
	{
        Body.GetComponent<SpriteRenderer>().color = new Color(0,0,1,1);
    }
    void Update()
    {
//Включение лазер        
        if (Input.GetMouseButtonDown(1) && UpdatePlayer.CheckUpdate == 0 )
        {
            if (LaserCheck == 0)
            {
                LaserCheck = 1;
                Body.GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
            }
            else
            {
                LaserCheck = 0;
                Body.GetComponent<SpriteRenderer>().color = new Color(0,0,1,1);
                Laser.transform.localPosition = new Vector2(0,0);
                Laser.transform.localScale = new Vector3(1,1,1);
                LaserPoint.transform.localPosition = new Vector3(0,0,0);
            }
        } 
        else if (UpdatePlayer.CheckUpdate == 1)
        {
            LaserCheck = 0;
            Body.GetComponent<SpriteRenderer>().color = new Color(0,0,1,1);
            Laser.transform.localPosition = new Vector2(0,0);
            Laser.transform.localScale = new Vector3(1,1,1);
            LaserPoint.transform.localPosition = new Vector3(0,0,0);
        }
        if(Global.EnergyCount <= 0)
        {
            LaserCheck = 0;
            Body.GetComponent<SpriteRenderer>().color = new Color(0,0,1,1);
            Laser.transform.localPosition = new Vector2(0,0);
            Laser.transform.localScale = new Vector3(1,1,1);
            LaserPoint.transform.localPosition = new Vector3(0,0,0);
        }
//Работа лазера
        if(LaserCheck == 1)
        {   
            
            Global.EnergyCount -= 1+(int)UpdatePlayer.Laser/5;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LaserRot.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - LaserRot.transform.position);
            if (Global.EnergyCount > 0)
            {
                LaserPoint.GetComponent<CircleCollider2D>().radius = UpdatePlayer.Laser/15;
                if(Laser.transform.localScale.y < UpdatePlayer.Laser)
                {
                    Laser.transform.localScale = new Vector3(0.5f,Laser.transform.localScale.y+0.5f,1);
                }
                else
                {
                    Laser.transform.localScale = new Vector3(0.5f,UpdatePlayer.Laser,1);
                }
                if(Laser.transform.localPosition.y < UpdatePlayer.Laser/7.1f)
                {
                    Laser.transform.localPosition = new Vector2(0,Laser.transform.localPosition.y+0.1f);
                }
                else
                {
                    Laser.transform.localPosition = new Vector2(0,UpdatePlayer.Laser/7.0f);
                }
            }
        }
    }
}