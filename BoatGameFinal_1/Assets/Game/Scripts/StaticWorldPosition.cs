using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticWorldPosition : MonoBehaviour
{
    Vector3 pos;
    bool isStart = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        transform.position = pos;
    }


    private void OnEnable()
    {
        this.Wait(() => {
            isStart = true;
            pos = transform.position;
        },.075f);
        
    }

    private void OnDisable()
    {
        isStart = false;
    }
}
