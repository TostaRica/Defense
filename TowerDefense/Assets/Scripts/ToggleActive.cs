using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleActive : MonoBehaviour
{
    public GameObject SubMenu;
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    void OnClick() 
    {
        if (SubMenu) SubMenu.SetActive(!SubMenu.activeSelf);
    } 
}
