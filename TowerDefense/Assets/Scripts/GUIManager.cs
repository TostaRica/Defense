using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GUIManager : MonoBehaviour
{
    public GameObject panel;
    public Camera camera;
    public GameObject NewBuildMenuPanel;
    public GameObject UpgradeBuildMenuPanel;
    public GameObject BuildButton;
    private Vector3 placedObjectWorldPosition = default(Vector3);
    private Vector2Int placedObjectOrigin = default(Vector2Int);
    public void Start()
    {
    }
    public void Update()
    {
        if (camera && panel) 
        { 

            if (Input.GetMouseButton(1))
            {
                CloseTowerMenu();
            }
        }
    }
    public void OpenTowerMenu(GameObject tower = null, Vector3 _placedObjectWorldPosition = default(Vector3), Vector2Int _placedObjectOrigin = default(Vector2Int))
    {
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
                NewBuildMenuPanel.SetActive(true);
                UpgradeBuildMenuPanel.SetActive(false);
                Button btn = BuildButton.GetComponent<Button>();
                btn.onClick.AddListener(Build);
                placedObjectWorldPosition = _placedObjectWorldPosition;
                placedObjectOrigin = _placedObjectOrigin;
            }
            else 
            {
                NewBuildMenuPanel.SetActive(false);
                UpgradeBuildMenuPanel.SetActive(true);
                
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
    void Build() {
        GridBuildingSystem.Instance.Build(placedObjectWorldPosition, placedObjectOrigin);
        panel.SetActive(false);
    }

}
