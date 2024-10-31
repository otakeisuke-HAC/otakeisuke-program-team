using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDamagePointController : MonoBehaviour
{
    private GameObject Boss;
    public GameObject[] PhaseObject;
    private int phaseCount = 0;
    private GameObject AirForce;
    public GameObject[] bossDamagePoint;

    public GameObject[] BatuLength;
    private bool nextPhaseFlag = false;
    private float nextPhaseCount = 3;
    public bool buthFlag = false;
    private int newBossHp = 2;
    
    public bool damagePoint = false;

    //Bossのサイレンを連続で鳴らさないようにするフラグ
    private bool sirenFlag= true;
    public AudioClip BossSiren;
    public AudioClip BossDie;

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.Find("Boss");
        AirForce = GameObject.Find("AirForce");
        
    }

    // Update is called once per frame
    void Update()
    {
        
        BatuLength = GameObject.FindGameObjectsWithTag("Batu");
        Debug.Log("batuオブジェクトのレングス" + BatuLength.Length);
        if (BatuLength.Length == 3)
        {
            for(int i = 0; i < BatuLength.Length; i++)
            {
                BatuLength[i].SetActive(false);
            }
            AirForce.GetComponent<SphereCollider>().enabled = true;
            nextPhaseFlag = true;
            PhaseObject[phaseCount].SetActive(false);
            //BossにあるAudioSourceから鳴らす
            if(sirenFlag == true && newBossHp > 0)
            {
                //BossにあるAudioSourceから鳴らす
                gameObject.transform.root.gameObject.GetComponent<BossCotroller>().BossAudioSorce.PlayOneShot(BossSiren);
                sirenFlag = false;
            }else if(sirenFlag == true&& newBossHp < 1)
            {
                //BossにあるAudioSourceから鳴らす
                gameObject.transform.root.gameObject.GetComponent<BossCotroller>().BossAudioSorce.PlayOneShot(BossDie);
                sirenFlag = false;
            }
            
        }
        if(nextPhaseFlag == true)
        {
            nextPhaseCount -= Time.deltaTime;
            if (nextPhaseCount <= 0)
            {
                for (int i = 0; i < bossDamagePoint.Length; i++)
                {
                    bossDamagePoint[i].SetActive(true);
                }
                AirForce.GetComponent<SphereCollider>().enabled = false;
                Boss.GetComponent<BossCotroller>().bossHP = newBossHp;
                nextPhaseFlag = false;
                phaseCount = phaseCount + 1;
                nextPhaseCount = 3;
            }
            else if (Boss.GetComponent<BossCotroller>().bossHP == newBossHp)
            {
                Debug.Log("HP減らす");
                newBossHp = newBossHp - 1;
                sirenFlag = true;
            }
        }
    }
}
