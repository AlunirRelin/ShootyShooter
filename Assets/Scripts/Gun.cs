using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactFx;
    public GameObject missFx;
    public GameObject DmgTextObj;
    public TextMeshPro DmgText;
    public PlayerMovementController playerMovementController;
    public Player player;
    public Enemy enemy;

    public bool auto;
    public float damage = 10f;
    public float fallofRange = 100f;
    public float fireRate = 15;
    public float FireCooldown = 0f;
    public float Spread = 0;
    public int pelletCount = 1;
    public int magazine = 6;
    public int currentMagazine;
    public float reloadSpeed;
    public bool reloading;
    public Vector3 dmgPos;
    public float damageDealt;
    public float currentDamage;
    private TowerBase selectedTower;
    public ButtonList buttonList;
    public void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
        cam= go.GetComponent<Camera>();
        playerMovementController = GetComponentInParent<PlayerMovementController>();
        player = GetComponentInParent<Player>();
        currentMagazine = magazine;
    }
    public virtual void Shoot()
    {
        currentDamage = damage;
        damageDealt = 0;
        RaycastHit hit;
        currentMagazine--;
        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 forwardVector = Vector3.forward;
            float deviation = Random.Range(0f, Spread);
            float angle = Random.Range(0f, 360f);
            forwardVector = Quaternion.AngleAxis(deviation, Vector3.up) * forwardVector;
            forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
            forwardVector = cam.transform.rotation * forwardVector;

            if (Physics.Raycast(cam.transform.position, forwardVector, out hit))
            {
                if(Vector3.Distance(cam.transform.position,hit.point) > fallofRange)
                {
                    Debug.Log(Vector3.Distance(cam.transform.position, hit.point));
                    currentDamage = ((damage / (fallofRange * Vector3.Distance(cam.transform.position, hit.point))) + damage) / 2;
                }
                enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Damage(currentDamage * player.playerDamage);
                    damageDealt += damage;
                    Instantiate(impactFx, hit.point, Quaternion.LookRotation(hit.normal));
                    dmgPos = hit.point;
                    dmgPos.y += 1;
                    DmgText.text = currentDamage.ToString("0");
                    Instantiate(DmgText, dmgPos, gameObject.transform.rotation);
                }
                else
                {
                    Instantiate(missFx, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }
    }
    public IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed * player.playerReload);
        currentMagazine = magazine;
        reloading = false;
    }
    public void TowerUpgrade()
    {
        GameObject buttonGO = GameObject.FindGameObjectWithTag("Buttons");
        buttonList = buttonGO.GetComponent<ButtonList>();
        RaycastHit hit;
        Vector3 forwardVector = Vector3.forward;
        forwardVector = cam.transform.rotation * forwardVector;

        if (Physics.Raycast(cam.transform.position, forwardVector, out hit))
        {
            Debug.Log("hit");
            selectedTower = hit.transform.GetComponent<TowerBase>();
            if (selectedTower != null)
            {
                if (!buttonList.listActive)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    ButtonList.selectedTower = selectedTower;
                    for (int i = 0; i < buttonList.transform.childCount; i++)
                    {
                        buttonList.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    buttonList.listActive = !buttonList.listActive;
                }
                else
                {
                    TakeDownList();
                }
            }
            else
            {
                if (buttonList.listActive)
                {
                    TakeDownList();
                }
            }
        }
        else
        {
            if (buttonList.listActive)
            {
                TakeDownList();
            }
        }

    }    
    public void TakeDownList()
    {
        Cursor.lockState = CursorLockMode.Locked;
        for (int i = 0; i < buttonList.transform.childCount; i++)
        {
            buttonList.transform.GetChild(i).gameObject.SetActive(false);
        }
        buttonList.listActive = !buttonList.listActive;
    }
}