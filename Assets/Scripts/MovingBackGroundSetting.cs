using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackGroundSetting : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
