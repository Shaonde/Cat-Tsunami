using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeStart : MonoBehaviour
{
    [SerializeField] GameObject[] ToSpawn;
    [SerializeField] float InitSpeed = 5f;
    [SerializeField] float DistanceToJump = 3f;
    [SerializeField] ScoreInGame Scoreur;
    public static PlateformeStart Instance;
    private List<GameObject> plateformes;
    private float _speed;

    private int NextIndex;    
    private GameObject LastSpawned
    {
        get => plateformes[plateformes.Count-1];
    }
    public int Score {get; private set;}

    public float GetActualSpeed() => _speed;

    void Awake()
    {
        if(Instance is null)
            Instance = this;
        else
        {
            Destroy(Instance);
            Instance = this;            
        }

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

    void SetSpeed()
    {
        foreach (GameObject item in plateformes)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.left * _speed;
        }
    }

    public void Bonus() 
    {
        Score+=3;
        Scoreur.Change(Score);
    }
    public void SelectNext() => NextIndex = Random.Range(0,ToSpawn.Length);

    public IEnumerator DeletePlat(GameObject plats)
    {
        yield return new WaitForSeconds(15f);
        plateformes.Remove(plats);
        Destroy(plats.gameObject);
    }

    private bool IsDistanceGood()
    {
        float a = LastSpawned.transform.position.x+(LastSpawned.transform.GetChild(0).localScale.x/2);

        if(a >= transform.position.x)
            return false;

        float b = transform.position.x-(ToSpawn[NextIndex].transform.GetChild(0).localScale.x/2);
        return (b-a) >= DistanceToJump;
    }

    void Update()
    {
        if(IsDistanceGood())
        {
            GameObject spawned = Instantiate(ToSpawn[NextIndex], transform.position, Quaternion.identity);
            plateformes.Add(spawned);
            spawned.GetComponent<Rigidbody>().velocity = Vector3.left * _speed;
            SelectNext();
            StartCoroutine(DeletePlat(spawned));
            _speed += .3f;
            SetSpeed();
            DistanceToJump += .3f;
            Score++;
            Scoreur.Change(Score);
        }
        
    }

}
