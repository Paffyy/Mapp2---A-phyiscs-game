﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstansiateMachine : MonoBehaviour {
    Animator anim;
    [Header("PREFABS ")] 
    public GameObject fanPrefab;
    public GameObject beltPrefab;
    public GameObject rocketPrefab;
    public GameObject flipPrefab;
    [Header("")]
    static bool sidePanelHide;
    public Transform machineSpawn;
    public GameController gameController;

    public int fanCount;
    public int beltCount;
    public int rocketCount;
    public int cannonCount;

    public Text fansLeft;
    public Text beltsLeft;
    public Text rocketsLeft;
    public Text cannonLeft;

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
    public void InstantiateMachine(GameObject prefab, Vector3 pos)
    {
        try
        {
            var gameObject = Instantiate(prefab);
            gameObject.transform.position = new Vector3 (pos.x,pos.y,gameObject.transform.position.z);
            gameObject.SetActive(true);
        }
        catch (System.Exception)
        {

            throw;
        }
    }
    public int GetCount(string prefabType)
    {
        if (prefabType.Contains("belt"))
        {
            return beltCount;
        }
        else if (prefabType.Contains("fan"))
        {
            return fanCount;
        }
        else if (prefabType.Contains("rocket"))
        {
            return rocketCount;
        }
        else if (prefabType.Contains("cannon"))
        {
            return cannonCount;
        }
        else
        {
            return 0;
        }
    }
    public void SetCount(string prefabType, Text count)
    {
        if (prefabType.Contains("belt"))
        {
            beltCount--;
            count.text = beltCount.ToString();

        }
        else if (prefabType.Contains("fan"))
        {
            fanCount--;
            count.text = fanCount.ToString();

        }
        else if (prefabType.Contains("rocket"))
        {
            rocketCount--;
            count.text = rocketCount.ToString();
        }
        else if (prefabType.Contains("cannon"))
        {
            cannonCount--;
            count.text = cannonCount.ToString();
        }
    }
    public void instantiateFan()
    {
        if (fanCount > 0)
        {
            Instantiate(fanPrefab, machineSpawn.position, machineSpawn.rotation);
            fanPrefab.SetActive(true);
            fanCount--;
        }
        fansLeft.text = fanCount.ToString();
        
    }
    public void instantiateBelt()
    {
        if (beltCount > 0)
        {
            Instantiate(beltPrefab, machineSpawn.position, machineSpawn.rotation);
            beltPrefab.SetActive(true);
            beltCount--;
        }
        beltsLeft.text = beltCount.ToString();
    }
}
