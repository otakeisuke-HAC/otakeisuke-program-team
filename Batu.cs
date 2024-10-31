using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Batu : MonoBehaviour
{
    public GameObject BossDamagePointController;
    private bool one = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.GetComponent<Image>().enabled == true && one == false)
        {
            one = true;
            
        }

        if(BossDamagePointController.GetComponent<BossDamagePointController>().buthFlag == true)
        {
            Debug.Log("jinooino");
            gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
            Invoke("one", 3);
        }
    }

    private void NextFlag()
    {
        one = false;
    }
}
