using UnityEngine;

public class UIDron : MonoBehaviour
{
    public GameObject _dron;
    public GameObject _storage;
    public GameObject _energy;
    RectTransform _rectTransformStorage;
    RectTransform _rectTransformEnergy;
    Vector2 _vector2Storage;
    Vector2 _vector2Energy;

    void Start()
    {
        _rectTransformStorage = _storage.GetComponent<RectTransform>();
        _rectTransformEnergy = _energy.GetComponent<RectTransform>();
        _vector2Storage = new Vector2(0, 1);
        _vector2Energy = new Vector2(0, 1);
    }

    void Update()
    {
        float test = (float)UpdatePlayer.storageCountMax / 7;
        float test2 = _dron.GetComponent<AutoMove>()._storageCount / test;
        _vector2Storage.x = test2;
        _rectTransformStorage.sizeDelta = _vector2Storage;

        float test3 = (float)UpdatePlayer.EnergyCountMax / 7;
        float test4 = _dron.GetComponent<AutoMove>()._energy / test3;
        if (test4 > 0)
        {
            _vector2Energy.x = test4;
            _rectTransformEnergy.sizeDelta = _vector2Energy;
        }
    }
}