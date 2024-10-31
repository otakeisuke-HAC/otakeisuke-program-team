using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject[] Items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Items = GameObject.FindGameObjectsWithTag("Item");
        if (Items.Length == 0)
        {
            Debug.Log("全クリ");
            gameController.GetComponent<GameController>().nextSceneflag = true;
        }
    }
}
