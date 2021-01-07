using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public static float fps;
    void OnGUI()
    {
        //Отображение фпс
        fps = 1.0f / Time.deltaTime;
        int i = (int) Mathf.Round(fps / 10) * 10;
        GUILayout.Label("FPS: " + i);
    }
}