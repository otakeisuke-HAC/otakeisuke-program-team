using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDathTorus : MonoBehaviour
{
    
    public GameController gameController;
    [HideInInspector]
    public bool safeBool = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Death();
            //gameController.playerCount--;
            Debug.Log("あたった");

        }
    }
}
