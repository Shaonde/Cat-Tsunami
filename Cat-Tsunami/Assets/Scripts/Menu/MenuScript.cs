using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play() => SceneManager.LoadScene(1);
    public void Quit() => Application.Quit();
}
