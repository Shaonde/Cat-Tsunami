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
        GameObject spawned1, spawned2;
        switch(_type)
        {
            case PlatType.OneBad:
                if(transform.childCount == 5)
                {
                    spawned1 = Instantiate(BadThings[UnityEngine.Random.Range(0,BadThings.Length)],
                    transform, true);
                    int aaa = UnityEngine.Random.Range(2,transform.childCount-2);
                    spawned1.transform.position = transform.GetChild(aaa).position;
                }
                break;
            
            case PlatType.TwoBad:
                if(transform.childCount == 5)
                {
                    int a = UnityEngine.Random.Range(2,transform.childCount-1);
                    int b = UnityEngine.Random.Range(2,transform.childCount-1);
                    while (b==a)
                        b = UnityEngine.Random.Range(2,transform.childCount-1);

                    spawned1 = Instantiate(BadThings[UnityEngine.Random.Range(0,BadThings.Length)],
                    transform,true);
                    spawned1.transform.position = transform.GetChild(a).position;

                    spawned2 = Instantiate(BadThings[UnityEngine.Random.Range(0,BadThings.Length)],
                    transform,true);
                    spawned2.transform.position = transform.GetChild(b).position;
                }
                break;

            case PlatType.OneNice:
                spawned1 = Instantiate(GoodThings[UnityEngine.Random.Range(0,GoodThings.Length)],
                transform,true);
                spawned1.transform.position = transform.GetChild(UnityEngine.Random.Range(2,transform.childCount-1)).position;

                break;
            
            case PlatType.OneNiceOneBad:
                if(transform.childCount == 5)
                {
                    int aa = UnityEngine.Random.Range(2,transform.childCount-1);
                    int bb = UnityEngine.Random.Range(2,transform.childCount-1);
                    while (bb==aa)
                        bb = UnityEngine.Random.Range(2,transform.childCount-1);
                    

                    spawned1 = Instantiate(BadThings[UnityEngine.Random.Range(0,BadThings.Length)],
                    transform,true);
                    spawned1.transform.position = transform.GetChild(aa).position;

                    spawned2 = Instantiate(GoodThings[UnityEngine.Random.Range(0,GoodThings.Length)],
                    transform,true);
                    spawned2.transform.position = transform.GetChild(bb).position;
                }
                break;
        }
    }
}
