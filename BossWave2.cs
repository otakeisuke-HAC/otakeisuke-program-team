using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave2 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wave2;

    public bool next2Flag = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        wave2 = GameObject.FindGameObjectsWithTag("Wave2Itimatu");
    }
}
