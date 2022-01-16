using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public GameObject panel;
    public Camera camera;
    public GameObject newBuildMenuPanel;
    public GameObject upgradeBuildMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject buildButton;
    public GameObject buildCancelButton;

    public Button btnResume;
    public Button btnRetry;
    public Button btnExit;

    public Text buildText;
    public Text wavesEnemiesText;
    public Text wavesNumberOfWavesText;

    public Button btnBallistaUpgrade;
    public Button btnCanonUpgrade;
    public Button btnCaulodron;

    public Button btnIncreaseSpeed;
    public Button btnIncreaseDamage;
    
    public Button btnFireUpgrade;
    public Button btnPoisonUpgrade;

    public ArrowIcon[] attackUIArrows;
    public ArrowIcon[] speedUIArrows;

    private Vector3 placedObjectWorldPosition = default(Vector3);
    private Vector2Int placedObjectOrigin = default(Vector2Int);
    private Button btnBuild;

    public Spawner spwaner;
    GameObject selectedTowerGO = null;
    TowerManager selectedTower = null;

    public void Start()
    {
        Time.timeScale = 1;
        if (buildButton) btnBuild = buildButton.GetComponent<Button>();
        if (btnBuild) btnBuild.onClick.AddListener(Build);
        if (btnBallistaUpgrade) btnBallistaUpgrade.onClick.AddListener(delegate { UpgradeTower(TowerManager.TowerType.Ballista); });
        if (btnCanonUpgrade) btnCanonUpgrade.onClick.AddListener(delegate { UpgradeTower(TowerManager.TowerType.Canon); });
        if (btnCaulodron) btnCaulodron.onClick.AddListener(delegate { UpgradeTower(TowerManager.TowerType.Caoldron); });
        if (btnIncreaseSpeed) btnIncreaseSpeed.onClick.AddListener(delegate { UpgradeTowerSpeed(); });
        if (btnIncreaseDamage) btnIncreaseDamage.onClick.AddListener(delegate { UpgradeTowerDamage(); });
        if (btnFireUpgrade) btnFireUpgrade.onClick.AddListener(delegate { SetElement(TowerManager.Type.Fire); });
        if (btnPoisonUpgrade) btnPoisonUpgrade.onClick.AddListener(delegate {SetElement(TowerManager.Type.Poison); });
        if (btnResume) btnResume.onClick.AddListener(TogglePause); 
        if (btnRetry) btnRetry.onClick.AddListener(Reload);
        if (btnExit) btnExit.onClick.AddListener(Exit);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            TogglePause();
        }
        if (wavesEnemiesText) wavesEnemiesText.text = Globals.currentWaveEnemies.Count.ToString();
        if (wavesNumberOfWavesText) wavesNumberOfWavesText.text = Globals.currentWaveNumber + "/" + Globals.totalNumberOfWaves;
    }
    public void TogglePause() {
        if (pauseMenuPanel)
        {
            Time.timeScale = 1 - Time.timeScale;
            pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
        }
    }
    public void OpenTowerMenu(GameObject tower = null, Vector3 _placedObjectWorldPosition = default(Vector3), Vector2Int _placedObjectOrigin = default(Vector2Int), bool refresh = false)
    {
        if (camera && panel)
        {
            panel.SetActive(true);
            if (!refresh) UpdatePanelPosition();
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
                if (selectedTower.activeTurret.towerType != TowerManager.TowerType.Basic) {
                    btnCanonUpgrade.enabled = false;
                    btnBallistaUpgrade.enabled = false;
                    btnCaulodron.enabled = false;
                }
                SetUIAttack(selectedTower.damageLvl);
                SetUISpeed(selectedTower.speedAttackLvl);
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
    private void UpdatePanelPosition() 
    {
        RectTransform rectTrans = panel.GetComponent<RectTransform>();
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        if (rectTrans.rect.size.x * rectTrans.transform.localScale.x + Input.mousePosition.x > camera.pixelWidth) mouseX -= rectTrans.rect.size.x * rectTrans.transform.localScale.x;
        if (rectTrans.rect.size.y * rectTrans.transform.localScale.y + Input.mousePosition.y > camera.pixelHeight) mouseY -= rectTrans.rect.size.y * rectTrans.transform.localScale.y;
        rectTrans.anchoredPosition = new Vector2(mouseX, mouseY);
    }
    void UpgradeTower(TowerManager.TowerType towerType) 
    {
        if (selectedTower) selectedTower.ChangeTower(towerType);
        OpenTowerMenu(selectedTowerGO, default(Vector3), default(Vector2Int), true);
    }
    void UpgradeTowerDamage()
    {
        if (selectedTower) selectedTower.UpgradeTowerDamage();
        OpenTowerMenu(selectedTowerGO, default(Vector3), default(Vector2Int), true);
    }
    void UpgradeTowerSpeed()
    {
        if (selectedTower) selectedTower.UpgradeTowerSpeed();
        OpenTowerMenu(selectedTowerGO, default(Vector3), default(Vector2Int), true);
    }
    void SetElement(TowerManager.Type element)
    {
        if (selectedTower) selectedTower.SetElement(element);
        OpenTowerMenu(selectedTowerGO, default(Vector3), default(Vector2Int), true);
    }
    void Build() {
        GridBuildingSystem.Instance.Build(placedObjectWorldPosition, placedObjectOrigin);
        Globals.updateMoney(-Globals.towerCost);
        Globals.numberOfTowers++;
        panel.SetActive(false);
    }
    void SetUISpeed(int speedLvl = 0) 
    {
        for (int i = 0; i < 3; i++) 
        {
            if (i < speedLvl) speedUIArrows[i].SelectColor(ArrowIcon.ArrowIconColor.Green);
            else speedUIArrows[i].SelectColor(ArrowIcon.ArrowIconColor.White);
        }
    }
    void SetUIAttack(int attackLvl = 0)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < attackLvl) attackUIArrows[i].SelectColor(ArrowIcon.ArrowIconColor.Green);
            else attackUIArrows[i].SelectColor(ArrowIcon.ArrowIconColor.White);
        }
    }
    void Reload()
    {
        SceneManager.LoadScene("MainScene");
    }
    void Exit() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
