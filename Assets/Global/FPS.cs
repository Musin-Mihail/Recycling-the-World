using UnityEngine;
public class FPS : MonoBehaviour
{
    public static float fps;
    void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        int i = (int) Mathf.Round(fps / 10) * 10;
        if(i >= 60)
        {
            i = 60;
        }
        GUILayout.Label("FPS: " + i);
    }
}