using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishSpot : MonoBehaviour
{
    [SerializeField] GameObject[] fishes;    
    
    void Start()
    {

       

    }


    public void Catch()
    {
        FindObjectOfType<Movement>().SetMove(false);
        UiManager.Instance.CatchingFish();

        StartCoroutine(endFishes());

        this.Wait(() => {
            EndCatch();
        },3.3f);
    }

    public void EndCatch()
    {
        FindObjectOfType<Movement>().SetMove(true);
        UiManager.Instance.EndCatchingFish();

        GameManager.instance.OnFishKilled();

        UiManager.Instance.SetPanelHintCatchFish(false);
        Destroy(gameObject);

    }


    IEnumerator endFishes()
    {
        foreach (var item in fishes)
        {

            yield return new WaitForSeconds(2.7f / fishes.Length);
            UiManager.Instance.AddScore();
                 item.SetActive(false);
        }
      


    }



    
}
