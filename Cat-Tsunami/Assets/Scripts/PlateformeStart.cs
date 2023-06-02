using System.Collections.Generic;
using UnityEngine;

public class PlateformeStart : MonoBehaviour
{
    [SerializeField] GameObject[] ToSpawn;
    [SerializeField] float InitSpeed = 5f;
    [SerializeField] float DistanceToJump = 5f;
    public static PlateformeStart Instance;
    private List<GameObject> plateformes;
    private float _speed;

    private int NextIndex;
    private float TimeBtwCheck = 1;
    
    private GameObject LastSpawned
    {
        get => plateformes[plateformes.Count-1];
    }

    void Awake()
    {
        if(Instance is null)
            Instance = this;
        else
            Destroy(this);

        _speed = InitSpeed;
        plateformes = new List<GameObject>();
        GameObject[] currents = GameObject.FindGameObjectsWithTag("Plateforme");
        foreach (GameObject item in currents)
        {
            plateformes.Add(item);
            item.GetComponent<Rigidbody>().velocity = Vector3.left * _speed;
        }
        SelectNext();
    }
    public void SelectNext() => NextIndex = Random.Range(0,ToSpawn.Length-1);

    public void DeletePlat(GameObject plats)
    {
        plateformes.Remove(plats);
        Destroy(plats.gameObject);
    }

    private bool IsDistanceGood()
    {
        float a = LastSpawned.transform.position.x+(LastSpawned.transform.localScale.x/2);

        if(a >= transform.position.x)
            return false;

        float b = transform.position.x-(ToSpawn[NextIndex].transform.localScale.x/2);
        return Mathf.Abs(b-a) >= DistanceToJump;
    }

    void Update()
    {
        if(IsDistanceGood())
        {
            GameObject spawned = Instantiate(ToSpawn[NextIndex], transform.position, Quaternion.identity);
            plateformes.Add(spawned);
            spawned.GetComponent<Rigidbody>().velocity = Vector3.left * _speed;
            SelectNext();
            _speed += .1f;
        }
        
        if(TimeBtwCheck <= 0) //TODO: remplacer ça par une coroutine
        {
            foreach (GameObject item in plateformes)
            {
                if(Vector3.Distance(item.transform.position, transform.position) >= 80f)
                    DeletePlat(item);
            }
            TimeBtwCheck = 1;
        }
        TimeBtwCheck -= Time.deltaTime;
    }
}
