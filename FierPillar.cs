using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FierPillar : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject FierObject;
    private BoxCollider Fierbox;
    [SerializeField]
    private GameObject Particle;

    //止めたときエフェクト
    public GameObject FierBreakEffect;

    // Start is called before the first frame update
    void Start()
    {
        Fierbox = FierObject.GetComponent<BoxCollider>();
        Fierbox.enabled = true;
        gameController = gameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Fierbox.enabled == true)
        {
            Fierbox.enabled = false;
            Particle.GetComponent<ParticleSystem>().Stop();
            Instantiate(FierBreakEffect, gameObject.transform.position,Quaternion.identity);
        }
        
    }
}
