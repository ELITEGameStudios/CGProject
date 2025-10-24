using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (main == null) { main = this; }
        else if(main != this){ Destroy(this); }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void RestartGame(){
        SceneManager.LoadScene(0);
    }
}
