using UnityEngine;
using TMPro;
public class FinalScore : MonoBehaviour
{
    void Start() => GetComponent<TMP_Text>().text = $"Ton score (de looser) est de : {PlayerPrefs.GetInt("score",0)}";
}
