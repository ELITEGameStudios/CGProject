using Unity.VisualScripting;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject winScreen;
    void OnTriggerEnter(Collider collider)
    {
        winScreen.SetActive(true);
        Player.instance.moveScript.enabled = false;
        Debug.Log("you win");
    }
}
