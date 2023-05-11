using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] GameObject hitrange;
    void Start()
    {
        
    }

    bool isHitFx = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.IsNameStartWith("Bullet"))
        {
            if (!isHitFx)
            {
                isHitFx = true;
                GetComponent<MeshRenderer>().material.DOColor(Color.red, .05f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => { isHitFx = false; });
            }
          

            health -= 25;

            if (health <= 0)
            {
                GameManager.instance.OnEnemyKilled();
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
