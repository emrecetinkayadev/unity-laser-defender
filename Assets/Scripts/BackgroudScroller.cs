using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroudScroller : MonoBehaviour
{
    [SerializeField] float backgroudScroolSpeed = 0.5f;
    Material renderer;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>().material;
        offset = new Vector2(0, backgroudScroolSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        renderer.mainTextureOffset += offset * Time.deltaTime;
    }



}
