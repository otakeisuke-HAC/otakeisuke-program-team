using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class Phase3Controller : MonoBehaviour
{
    public GameObject Wave1;
    public GameObject Wave2;

    public float changCount = 3;
    public bool chang = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(chang == true)
        {
            changCount += Time.deltaTime;
        }
        else
        {
            changCount -= Time.deltaTime;
        }

        if (changCount <= 0)
        {
            chang = true;
        }
        else if(changCount >= 3)
        {
            chang = false;
        }

        if (chang == true)
        {
            Wave1.SetActive(true);
            Wave2.SetActive(false);
        }else if(chang == false)
        {
            Wave1.SetActive(false);
            Wave2.SetActive(true);
        }
    }
}
