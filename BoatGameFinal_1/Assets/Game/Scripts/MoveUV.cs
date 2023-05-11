using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MoveUV : MonoBehaviour
{
    // Scroll main texture based on time

    [SerializeField] float scrollSpeed = 0.5f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.sharedMaterial.SetTextureOffset("_BaseMap", new UnityEngine.Vector2(offset, 0));
    }





}
