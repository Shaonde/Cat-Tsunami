using UnityEngine;
using TMPro;

public class ScoreInGame : MonoBehaviour
{
    private TMP_Text _tm;
    
    void Start() => _tm = GetComponent<TMP_Text>();
    public void Change(int score) => _tm.text = score.ToString();
}
