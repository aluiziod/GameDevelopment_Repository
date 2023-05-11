using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class StaticWorldRotation : MonoBehaviour
{
    [SerializeField] RectTransform obj;
    [SerializeField] Vector3 rotation = Vector3.zero;
    Camera cameraToLookAt;

    void Start()
    {
        cameraToLookAt = Camera.main;
        
    }


    void Update()
    {
        try
        {
            Quaternion lookRotation = cameraToLookAt.transform.rotation;
            transform.rotation = lookRotation;
        }
        catch { }
    }
}    