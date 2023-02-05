using UnityEngine;
using UnityEngine.UI;

public class Text2 : MonoBehaviour
{
    // public Text Yellow;
    // public Text Red;
    // public Text Blue;
    public Text YellowBase;
    public Text RedBase;
    public Text BlueBase;

    public Text MagentaBase;

    // public Text Energy;
    // public Text Storage;
    public GameObject Win;

    public GameObject UpdateStat;
    // int Check = 1;

    void Update()
    {
        // Red.text = ": " + Global.Red;
        // Yellow.text = ": " + Global.Yellow;
        // Blue.text = Global.BlueBase + " / 20";
        RedBase.text = ": " + Global.RedBase;
        YellowBase.text = ": " + Global.YellowBase;
        BlueBase.text = ": " + Global.BlueBase;
        MagentaBase.text = ": " + Global.BaseMagenta;
        // Energy.text = Global.EnergyCount + " / " + UpdatePlayer.EnergyCountMax;
        // Storage.text = Global.storageCount + " / " + UpdatePlayer.storageCountMax;
        // if(Global.BlueBase>=20 && Check == 1)
        // {
        //     Win.SetActive(true);
        //     Check = 0;
        // }
    }

    public void WinDisable()
    {
        Win.SetActive(false);
    }

    public void UpdateEnable()
    {
        UpdateStat.SetActive(true);
        UpdatePlayer.CheckUpdate = 1;
    }

    public void UpdateDisable()
    {
        UpdateStat.SetActive(false);
        UpdatePlayer.CheckUpdate = 0;
    }
}