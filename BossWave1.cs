using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class BossWave1 : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] wave1;

    public bool next1Flag;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        wave1 = GameObject.FindGameObjectsWithTag("Wave1Itimatu");
    }
}
