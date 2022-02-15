using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowIcon : MonoBehaviour
{
    public RawImage greenArrow;
    public RawImage grayArrow;
    public RawImage whiteArrow;
    public enum ArrowIconColor{
        White, Gray, Green
    }
    // Start is called before the first frame update
    void Start()
    {
        SelectColor(ArrowIconColor.White);
    }
    public void SelectColor(ArrowIconColor color) 
    {
        if(greenArrow && greenArrow && greenArrow) 
        {
            switch (color)
            {
                case ArrowIconColor.Green:
                    greenArrow.enabled = true;
                    grayArrow.enabled = false;
                    whiteArrow.enabled = false;
                    break;
                case ArrowIconColor.Gray:
                    greenArrow.enabled = false;
                    grayArrow.enabled = true;
                    whiteArrow.enabled = false;
                    break;
                case ArrowIconColor.White:
                    greenArrow.enabled = false;
                    grayArrow.enabled = false;
                    whiteArrow.enabled = true;
                    break;
            }
        }

    }
}
