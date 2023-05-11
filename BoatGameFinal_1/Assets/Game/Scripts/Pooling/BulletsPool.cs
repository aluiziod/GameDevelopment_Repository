using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{

    [SerializeField] GameObject rifleBullet;
    Stack<GameObject> riflePool = new Stack<GameObject>();
   

    // Start is called before the first frame update
    void Start()
    {
       
    }


    /// <summary>
    /// Get a Active Bullet Game Object from Pool
    /// if not available in the pool then Instantiate it.
    /// </summary>
    /// <returns></returns>
    public GameObject GetBullet(bool isActive=true,bool isParent=true)
    {
        //print(riflePool.Count);

        GameObject b = null;

        if (riflePool.Count > 0)        
           b = riflePool.Pop();         
        else
        {
            b = Instantiate(rifleBullet);
            if(isParent)
            b.transform.SetParent(transform);
        }


        b.SetActive(isActive);

        return b;
    }

    /// <summary>
    /// Add Bullet Gameobject to Pool.
    /// </summary>
    /// <param name="b"></param>
    public void ReturnToPool(GameObject b)
    {
        b.SetActive(false);
        riflePool.Push(b);

//        print("sss"+riflePool.Count);
    }
    
}
