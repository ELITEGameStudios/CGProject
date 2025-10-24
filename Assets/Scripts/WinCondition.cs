using Unity.VisualScripting;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("you win");
    }
}
