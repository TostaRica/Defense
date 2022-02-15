using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMovement : MonoBehaviour
{

    public Material lavaMaterial;
    public bool isLavaMoving = false;
    public Vector2 offset = new Vector2(0,0);

    Vector2 currentOffset;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLavaMoving)
        {
            currentOffset += offset * Time.deltaTime; 
            lavaMaterial.SetFloat("_offsetX", currentOffset.x);
            lavaMaterial.SetFloat("_offsetY", currentOffset.x);
        }
    }


    void SetIsLavaMoving(bool _isLavaMoving)
    {
        isLavaMoving = _isLavaMoving;
    }
}
