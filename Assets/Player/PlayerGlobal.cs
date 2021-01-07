using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobal : MonoBehaviour
{
    public static GameObject Player;
    void Awake()
    {
        Player = gameObject;
    }
    void Start()
	{
        Application.targetFrameRate = 60;
        Cursor.visible = false;
    }
}