using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonList : MonoBehaviour
{
    [SerializeField]
    private UpgradeButton[] buttons;
    public static TowerBase selectedTower;
    public bool listActive;

    private void Start()
    {
        selectedTower = FindObjectOfType<TowerBase>();
    }
    private void OnEnable()
    {
        selectedTower = FindObjectOfType<TowerBase>();
        Cursor.lockState = CursorLockMode.Confined;
        foreach (UpgradeButton button in buttons)
        {
            button.Upgrade = (int bazinga) => { };
            button.Upgrade = selectedTower.UpdateTower;
        }
    }
    private void OnDisable()
    {
        if (Cursor.lockState == CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
