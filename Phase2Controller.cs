using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Controller : MonoBehaviour
{
    public GameObject[] Torus;

    private float attackCount = 2;
    //次のエリアオブジェクトの数
    private int nexterea = -1;
    private bool attackFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Torus.Length; i++)
        {
            Torus[i] = transform.GetChild(i).gameObject;
            Debug.Log(Torus[i]);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (attackCount <= 0 && attackFlag == true)
        {
            nexterea = nexterea + 1;
            Torus[nexterea].SetActive(true);
            attackCount = 2;
            attackFlag = false;

        }
        else if(attackFlag == true)
        {
            attackCount -= Time.deltaTime;
            
        }

        if(attackFlag == false && Torus[nexterea].activeSelf == false)
        {
            attackFlag = true;
            
            
        }

    }

    //public void Okureru()
    //{
    //    nexterea = -1;
    //}
}
