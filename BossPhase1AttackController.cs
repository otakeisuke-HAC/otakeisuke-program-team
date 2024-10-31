using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPhase1AttackController : MonoBehaviour
{
    private float attackCount = 5;
    private BoxCollider boxCollider;
    public AudioClip attackClip;
    //音を一度だけ鳴らすフラグ
    private bool audioStart;
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        attackCount -= Time.deltaTime;
        

        if(attackCount <= 0)
        {
            boxCollider.enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Invoke("AttackReset",1);
            transform.GetChild(0).gameObject.SetActive(true);
            if(audioStart == true)
            {
                //BossにあるAudioSourceから鳴らす
                gameObject.transform.root.gameObject.GetComponent<BossCotroller>().BossAudioSorce.PlayOneShot(attackClip);
                audioStart = false;
            }
            
        }
        else
        {
            audioStart = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }

    private void AttackReset()
    {
        boxCollider.enabled = false;
        attackCount = 5;
        gameObject.SetActive(false);
        
    }
    
        
}
