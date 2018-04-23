using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstansiateMachine : MonoBehaviour {
    public Button theButton;


    public GameObject fanPrefab;
    public Transform machineSpawn;

    private void Start()
    {
        Button btn = theButton.GetComponent<Button>();
        btn.onClick.AddListener(wichMachine);
    }

    public void wichMachine()
    {

    }

    public void instantiateFan()
    {
        Instantiate(fanPrefab, machineSpawn.position, machineSpawn.rotation);
    }
}
