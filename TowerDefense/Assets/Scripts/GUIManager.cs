using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public GameObject panel;
    public Camera camera;

    public void Start()
    {
    }
    public void Update()
    {
        if (Input.GetMouseButton(0)) {
            OpenTowerMenu();
        }
        if (Input.GetMouseButton(1))
        {
            CloseTowerMenu();
        }
    }
    public void OpenTowerMenu(GameObject tower = null){
            if (camera && panel && !panel.activeSelf)
            {
                panel.SetActive(true);
                Vector3 mousePosition = camera.GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
                panel.GetComponent<RectTransform>().pivot = new Vector2(mousePosition.x, mousePosition.y);
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
