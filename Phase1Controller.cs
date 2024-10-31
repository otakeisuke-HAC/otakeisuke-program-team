using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Controller : MonoBehaviour
{
    private GameObject Phase1AttackUpRight;
    private GameObject Phase1AttackUpLeft;
    private GameObject Phase1AttackDownRiget;
    private GameObject Phase1AttackDownLeft;

    private float phase1AttackCount = 3;
    private bool attackFlag = true;
    //アタックする場所をランダムで指定する変数
    private int randomAttackErea;
    //ランダムで出した値のオブジェクトを一時的に代入するためのオブジェクト
    private GameObject NowAttackObject;


    // Start is called before the first frame update
    void Start()
    {
        //右上
        Phase1AttackUpRight = transform.GetChild(0).gameObject;
        //左上
        Phase1AttackUpLeft = transform.GetChild(1).gameObject;
        //右下
        Phase1AttackDownRiget = transform.GetChild(2).gameObject;
        //左下
        Phase1AttackDownLeft = transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(phase1AttackCount <= 0 && attackFlag == true)
        {
            randomAttackErea = Random.Range(0, 4);
            if(randomAttackErea == 0)
            {
                Phase1AttackDownLeft.SetActive(true);
                NowAttackObject = Phase1AttackDownLeft;
            }else if(randomAttackErea == 1)
            {
                Phase1AttackDownRiget.SetActive(true);
                NowAttackObject = Phase1AttackDownRiget;
            }else if(randomAttackErea == 2)
            {
                Phase1AttackUpLeft.SetActive(true);
                NowAttackObject = Phase1AttackUpLeft;
            }else
            {
                Phase1AttackUpRight.SetActive(true);
                NowAttackObject = Phase1AttackUpRight;
            }
            
            attackFlag = false;
            phase1AttackCount = 5;
        }
        else if(attackFlag == true)
        {
            phase1AttackCount -= Time.deltaTime;
        }

        if(attackFlag == false && NowAttackObject.activeSelf == false)
        {

            attackFlag = true;
            
            Debug.Log(NowAttackObject);
        }

    }
}
