using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamagePoint : MonoBehaviour
{
    [SerializeField]
    private GameObject Boss;
    

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.Find("Boss");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
            gameObject.SetActive(false);
            
        }
    }
}
