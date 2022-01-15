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
    public GameObject newBuildMenuPanel;
    public GameObject upgradeBuildMenuPanel;
    public GameObject buildButton;
    public GameObject buildCancelButton;
    public Text buildText;
    private Vector3 placedObjectWorldPosition = default(Vector3);
    private Vector2Int placedObjectOrigin = default(Vector2Int);
    private Button btnBuild;
    TowerManager selectedTower = null;
    public void Start()
    {
        btnBuild = buildButton.GetComponent<Button>();
        if (btnBuild) btnBuild.onClick.AddListener(Build);
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
                newBuildMenuPanel.SetActive(true);
                upgradeBuildMenuPanel.SetActive(false);

                    if (buildButton && buildText && buildCancelButton)
                    {
                        if (Globals.getMoney() >= Globals.towerCost)
                        {
                            buildText.text = "Wanna build a tower here for "+ Globals.towerCost +" Gold?";
                            placedObjectWorldPosition = _placedObjectWorldPosition;
                            placedObjectOrigin = _placedObjectOrigin;
                            buildButton.SetActive(true);
                            buildCancelButton.SetActive(true);
                    }
                        else {
                            buildText.text = "Not enough gold to build a tower";
                            buildButton.SetActive(false);
                            buildCancelButton.SetActive(false);
                        }
                    }
            }
            else //Tower Upgrades
            {
                newBuildMenuPanel.SetActive(false);
                upgradeBuildMenuPanel.SetActive(true);
                selectedTower = tower.GetComponent<TowerManager>();
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
        Globals.updateMoney(-Globals.towerCost);
        Globals.numberOfTowers++;
        panel.SetActive(false);
    }

}
