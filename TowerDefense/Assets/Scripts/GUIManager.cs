using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GUIManager : MonoBehaviour
{
    public GameObject panel;
    public Camera camera;
    public void Start()
    {
    }
    public void Update()
    {
        if (camera && panel) 
        { 
            if (Input.GetMouseButtonDown(0) )
            {
                //click fuera
                if (!Globals.IsPointOverUIObject())
                {

                    if (panel.activeSelf) 
                    {
                        CloseTowerMenu();
                    }
                    else
                    {
                        OpenTowerMenu();
                    }
                }
            }

            if (Input.GetMouseButton(1))
            {
                CloseTowerMenu();
            }
        }
    }
    public void OpenTowerMenu(GameObject tower = null){
        if (camera && panel)
        {
            RectTransform rectTrans = panel.GetComponent<RectTransform>();
            panel.SetActive(true);
            float mouseX = Input.mousePosition.x;
            float mouseY= Input.mousePosition.y;
            if (rectTrans.sizeDelta.x + Input.mousePosition.x > camera.pixelWidth) mouseX -= rectTrans.sizeDelta.x;
            if (rectTrans.sizeDelta.y + Input.mousePosition.y > camera.pixelHeight) mouseY -= rectTrans.sizeDelta.y;
            rectTrans.anchoredPosition = new Vector2(mouseX, mouseY);
            if (!tower)
            {

            }
            else 
            {
                
            } 
        }
    }
    public void CloseTowerMenu()
    {
            if (panel.activeSelf)
            {
                panel.SetActive(false);
            }
    }

}
