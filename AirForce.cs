using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirForce : MonoBehaviour
{
    public float coefficient;   // 空気抵抗係数
    public float speed;         // 爆風の速さ

    public ParticleSystem Smock;
    private bool next = false;  

    void OnTriggerStay(Collider col)
    {
        if (col.attachedRigidbody == null)
        {
            return;
        }

        // 風速計算
        var velocity = (col.transform.position - transform.position).normalized * speed;

        // 風力与える
        col.attachedRigidbody.AddForce(coefficient * velocity);
    }

    private void Update()
    {
        if(gameObject.GetComponent<SphereCollider>().enabled == true && next == false)
        {
            Smock.Play();
            next = true;
            Invoke("SmockS", 3f);
        }


    }

    void SmockS()
    {
        next = false;
    }
}
