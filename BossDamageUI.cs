using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BossDamageUI : MonoBehaviour
{
    public GameObject Boss;
    [SerializeField]
    private Transform targetTfm;
    
    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 0f, 0);
    private float teimetu = 10;

    private GameObject Batu;
    


    void Start()
    {
        Boss = GameObject.Find("Boss");
        myRectTfm = GetComponent<RectTransform>();
        Batu = transform.GetChild(0).gameObject;
        //Batu.SetActive(false);
    }

    void Update()
    {
        myRectTfm.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);

    }
    private void FixedUpdate()
    {
        if (targetTfm.gameObject.activeSelf == false)
        {
            teimetu++;
            var image = gameObject.GetComponent<Image>();
            int count = 20;
            if (teimetu < count)
            {
                image.enabled = false;
            }
            else if (teimetu < count * 2)
            {
                image.enabled = true;
            }
            else if (teimetu < count * 3)
            {
                image.enabled = false;
            }
            else
            {
                image.enabled = true;
                Batu.SetActive(true);
            }

        }
        else if (targetTfm.gameObject.activeSelf == true)
        {
            teimetu = 0;
        }
    }
}
