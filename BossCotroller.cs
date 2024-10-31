using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCotroller : MonoBehaviour
{
    public enum BossPhase
    {
        BossPhase1, BossPhase2, BossPhase3
    }BossPhase bossPhase;

    private GameController gameController;

    private GameObject Phase1;
    private GameObject Phase2;
    private GameObject Phase3;
    //ボスのダメージ
    public int bossHP = 3;
    public GameObject ClearCanvas;
    //ぼすのSE
    private bool bossBGMFlag;
    public AudioClip BossChange;
    
    [HideInInspector]
    public AudioSource BossAudioSorce;

    //ボスbgm
    private GameObject BossBGM;

    // Start is called before the first frame update
    void Start()
    {
        bossPhase = BossPhase.BossPhase1;
        Phase1 = transform.GetChild(0).gameObject;
        Phase2 = transform.GetChild(1).gameObject;
        Phase3 = transform.GetChild(2).gameObject;
        Phase1.SetActive(true);
        gameController = FindObjectOfType<GameController>();
        BossAudioSorce = GetComponent<AudioSource>();
        bossBGMFlag = true;
        //ボス戦のbgm
        BossBGM = GameObject.Find("Audio");
    }

    // Update is called once per frame
    void Update()
    {
        BossAttackPhase();
        if(bossBGMFlag == true)
        {
            Invoke("BossCangeSE", 0.1f);
            bossBGMFlag = false;
            BossAudioSorce.Stop();
        }
    }
    void BossCangeSE()
    {
        BossAudioSorce.PlayOneShot(BossChange);
    }

    void BossAttackPhase()
    {
        if (bossPhase == BossPhase.BossPhase1)
        {            
            if (bossHP == 2)
            {
                Phase1.SetActive(false);
                Phase2.SetActive(true);
                bossPhase = BossPhase.BossPhase2;
                bossBGMFlag = true ;
                
            }
        }
        else if(bossPhase == BossPhase.BossPhase2)
        {
            
            if(bossHP == 1)
            {
                Phase2.SetActive(false);
                Phase3.SetActive(true);
                bossPhase = BossPhase.BossPhase3 ;
                bossBGMFlag = true ;
            }
        }
        else if(bossPhase== BossPhase.BossPhase3)
        {
            if (bossHP == 0)
            {
                Destroy(gameObject);
                Destroy(BossBGM);
                gameController.nextSceneflag = true;
                ClearCanvas.SetActive(true);
            }
        }
    }
}
