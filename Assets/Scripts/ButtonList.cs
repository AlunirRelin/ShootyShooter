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
    public void OnChoose()
    {
        selectedTower = FindObjectOfType<TowerBase>();
        Cursor.lockState = CursorLockMode.Confined;
        if (selectedTower == null)
        {
            return;
        }
        foreach (UpgradeButton button in buttons)
        {
            button.Upgrade = (int bazinga) => { };
            Debug.Log(button.transform.name);
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
