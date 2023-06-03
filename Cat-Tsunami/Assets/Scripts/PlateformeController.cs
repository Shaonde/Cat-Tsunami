using System;
using UnityEngine;

public class PlateformeController : MonoBehaviour
{
    private enum PlatType
    {
        OneBad,
        TwoBad,
        OneNice,
        OneNiceOneBad,
        Nothing
    }

    [SerializeField] GameObject[] BadThings;
    [SerializeField] GameObject[] GoodThings;
    private PlatType _type;

    void Awake()
    {
        Array values = Enum.GetValues(typeof(PlatType));
        _type = (PlatType)values.GetValue(UnityEngine.Random.Range(0,values.Length));
        switch(_type)
        {
            case PlatType.OneBad:
                transform.GetChild(UnityEngine.Random.Range(1,transform.hierarchyCount));
                break;
            
            case PlatType.TwoBad:
                break;

            case PlatType.OneNice:
                break;
            
            case PlatType.OneNiceOneBad:
                break;

            case PlatType.Nothing:
                break;
        }
    }
}
