using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Services.Analytics.Internal;

public class PlayerCollision : MonoBehaviour
{

    public bool IsHitRange = false;
    public Transform hitObj = null;

    public bool IsFishSpot = false;
    public FishSpot fishSpot;

    [SerializeField] MeshRenderer boatMesh;
    [SerializeField] int health = 100;



    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        
        if (other.IsNameStartWith("Enemy"))
        {
            boatMesh.sharedMaterial.DOColor(Color.red, .1f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
             health -= 25;

            UiManager.Instance.SetHealth(health);

            if (health <= 0)
            {
                GameManager.instance.OnLevelFailed();
            }

        }


        if (other.IsNameStartWith("BossEnemy"))
        {
            boatMesh.sharedMaterial.DOColor(Color.red, .1f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
            health -= 100;

            UiManager.Instance.SetHealth(health);

            if (health <= 0)
            {
                GameManager.instance.OnLevelFailed();
                other.transform.parent.parent.GetComponent<BossEnemy>().JumpToPlayer(transform);
            }

        }



    }



    private void OnTriggerStay(Collider other)
    {
        if (other.IsNameStartWith("HitRange"))
        {
            IsHitRange = true;
            hitObj = other.transform.parent;
            UiManager.Instance.SetPanelHintShoot(true);
        }


        if (other.IsNameStartWith("FisheSpot"))
        {
            //other.enabled = false;
            //other.GetComponent<FishSpot>().Catch();

            IsFishSpot = true;
            fishSpot = other.GetComponent<FishSpot>();
            UiManager.Instance.SetPanelHintCatchFish(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.IsNameStartWith("HitRange"))
        {
            IsHitRange = false;
            UiManager.Instance.SetPanelHintShoot(false);
        }

        if (other.IsNameStartWith("FisheSpot"))
        {
           

            IsFishSpot = false;
            fishSpot = null;
            UiManager.Instance.SetPanelHintCatchFish(false);
        }


    }


}
