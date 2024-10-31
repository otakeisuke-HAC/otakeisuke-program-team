using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase3AttackController : MonoBehaviour
{
    [HideInInspector]
    public GameObject Phase3;
    private float attackCount = 2;
    private BoxCollider boxCollider;
    public bool endflag = false;
    public AudioClip attackClip;
    //音を一度だけ鳴らすフラグ
    private bool audioStart;
    // Start is called before the first frame update
    void Start()
    {
        Phase3 = GameObject.Find("Phase3");
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        attackCount -= Time.deltaTime;
        if (attackCount <= 0)
        {
            boxCollider.enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Invoke("AttackReset", 1);
            transform.GetChild(0).gameObject.SetActive(true);
            if (audioStart == true && attackClip != null)
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
        attackCount = 2;
        
    }
}
