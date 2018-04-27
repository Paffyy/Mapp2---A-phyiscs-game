using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstansiateMachine : MonoBehaviour {
    Animator anim;
    static bool sidePanelHide;

    public Button theButton;
    public GameObject fanPrefab;
    public GameObject beltPrefab;
    public Transform machineSpawn;
    public GameController gameController;
    public int fanCount;
    public int beltCount;
    private void Start()
    {
        anim = GetComponent<Animator>();
        sidePanelHide = true;
    }

    public void animTriggers()
    {
        Debug.Log("click MF");
        if (anim.GetBool("sidePanelShow"))
        {
            anim.SetBool("sidePanelShow", false);
        }
        else
        {
            anim.SetBool("sidePanelShow", true);
        }
    }
    public void SetAnimBool()
    {
        anim.SetBool("sidePanelShow", true);
    }
    public void instantiateFan()
    {
        if (fanCount > 0)
        {
            Instantiate(fanPrefab, machineSpawn.position, machineSpawn.rotation);
            fanPrefab.SetActive(true);
            fanCount--;
        }
    }
    public void instantiateBelt()
    {
        if (beltCount > 0)
        {
            Instantiate(beltPrefab, machineSpawn.position, machineSpawn.rotation);
            beltPrefab.SetActive(true);
            beltCount--;
        }
    }
}
