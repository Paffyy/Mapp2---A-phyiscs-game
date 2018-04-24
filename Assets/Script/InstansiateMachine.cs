using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstansiateMachine : MonoBehaviour {
    Animator anim;
    static bool sidePanelHide;

    public Button theButton;
    public GameObject fanPrefab;
    public GameObject machinePrefab;
    public Transform machineSpawn;

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
    
    public void instantiateFan()
    {
        Instantiate(machinePrefab, machineSpawn.position, machineSpawn.rotation);
    }
}
