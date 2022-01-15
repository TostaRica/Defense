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

    public Button btnBallistaUpgrade;
    public Button btnCanonUpgrade;
    public Button btnCaulodron;

    public Button btnIncreaseSpeed;
    public Button btnIncreaseDamage;
    
    public Button btnFireUpgrade;
    public Button btnPoisonUpgrade;

    private Vector3 placedObjectWorldPosition = default(Vector3);
    private Vector2Int placedObjectOrigin = default(Vector2Int);
    private Button btnBuild;

    GameObject selectedTowerGO = null;
    TowerManager selectedTower = null;
    public void Start()
    {
        if (buildButton) btnBuild = buildButton.GetComponent<Button>();
        if (btnBuild) btnBuild.onClick.AddListener(Build);
        if (btnBallistaUpgrade) btnBallistaUpgrade.onClick.AddListener(delegate { UpgradeTower(TowerManager.TowerType.Ballista); });
        if (btnCanonUpgrade) btnCanonUpgrade.onClick.AddListener(delegate { UpgradeTower(TowerManager.TowerType.Canon); });
        //btnCaulodron.onClick.AddListener(delegate { UpgradeTower(TowerManager.TowerType.Caoldron); });
        if (btnIncreaseSpeed) btnIncreaseSpeed.onClick.AddListener(delegate { UpgradeTowerSpeed(); });
        if (btnIncreaseDamage) btnIncreaseDamage.onClick.AddListener(delegate { UpgradeTowerDamage(); });
       if (btnFireUpgrade) btnFireUpgrade.onClick.AddListener(delegate { SetElement(TowerManager.Type.Fire); });
       if (btnPoisonUpgrade) btnPoisonUpgrade.onClick.AddListener(delegate {SetElement(TowerManager.Type.Poison); });
    }
    public void Update()
    {

    }
    public void OpenTowerMenu(GameObject tower = null, Vector3 _placedObjectWorldPosition = default(Vector3), Vector2Int _placedObjectOrigin = default(Vector2Int))
    {
        if (camera && panel)
        {
            RectTransform rectTrans = panel.GetComponent<RectTransform>();
            panel.SetActive(true);
            float mouseX = Input.mousePosition.x;
            float mouseY= Input.mousePosition.y;
            Debug.Log(rectTrans.rect.size);
            if (rectTrans.rect.size.x * rectTrans.transform.localScale.x + Input.mousePosition.x > camera.pixelWidth) mouseX -= rectTrans.rect.size.x * rectTrans.transform.localScale.x;
            if (rectTrans.rect.size.y * rectTrans.transform.localScale.y + Input.mousePosition.y > camera.pixelHeight) mouseY -= rectTrans.rect.size.y * rectTrans.transform.localScale.y;
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
                selectedTowerGO = tower;
                selectedTower = tower.GetComponent<TowerManager>();
                //if (selectedTower.activeTurret.towerType != TowerManager.TowerType.Basic) {
                //    btnCanonUpgrade.enabled = false;
                //    btnBallistaUpgrade.enabled = false;
                //    btnCaulodron.enabled = false;
                //}
                //if (selectedTower.activeTurret.type != TowerManager.Type.Neutral)
                //{
                //    btnFireUpgrade.enabled = false;
                //    btnPoisonUpgrade.enabled = false;
                //}

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
    void UpgradeTower(TowerManager.TowerType towerType) 
    {
        if (selectedTower) selectedTower.ChangeTower(towerType);
        //OpenTowerMenu(selectedTowerGO);

    }
    void UpgradeTowerDamage()
    {
        if (selectedTower) selectedTower.UpgradeTowerDamage();
        //OpenTowerMenu(selectedTowerGO);
    }
    void UpgradeTowerSpeed()
    {
        if (selectedTower) selectedTower.UpgradeTowerSpeed();
        //OpenTowerMenu(selectedTowerGO);
    }
    void SetElement(TowerManager.Type element)
    {
        if (selectedTower) selectedTower.SetElement(element);
        //OpenTowerMenu(selectedTowerGO);
    }
    void Build() {
        GridBuildingSystem.Instance.Build(placedObjectWorldPosition, placedObjectOrigin);
        Globals.updateMoney(-Globals.towerCost);
        Globals.numberOfTowers++;
        panel.SetActive(false);
    }
    

}
