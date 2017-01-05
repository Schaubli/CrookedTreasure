﻿using UnityEngine;
using System.Collections;

public class VRhandler : MonoBehaviour {
    [HideInInspector]
    public int mode;
    [HideInInspector]
    public int enemy;
    [HideInInspector]
    public int island;

    public GameObject monsterPrefab;
    public GameObject enemyShipPrefab;
    public GameObject islandPrefab0;
    private GameObject monsterVRGameObject = null;
    private GameObject shipVRGameObject = null;
    private VREnemyKrake krake = null;
    private VREnemyShip enemyShip = null;
    public GameObject cameraRig;
    private bool allowedToLeave = false;
    // public GameObject shipPrefab;

    public GameObject lootCratePrefab;

    public GameObject actionIcon_crosshair_prefab;
    public GameObject actionIcon_steeringwheel_prefab;
    public GameObject actionIcon_shovel_prefab;
    private GameObject actionIcon_crosshair_go = null;
    private GameObject actionIcon_steeringwheel_go = null;
    private GameObject actionIcon_shovel_go = null;
    private GameObject[] actionIcons = null;

    private bool alreadydead = false;

    void Start()
    {
        if (monsterPrefab == null)
        {
            Debug.LogError("monsterPrefab is not defined in VRhandler");
        }
        if (enemyShipPrefab == null)
        {
            Debug.LogError("enemyShipPrefab is not defined in VRhandler");
        }
    }

    public void initVR () {
       
        switch (this.mode)
        {
            case 1:     // Fight

                Debug.Log("Initiate VR Fight");

                switch (this.enemy) {

                    case 0: //Monster                     
                        // init monster
                        monsterVRGameObject = (GameObject)Instantiate(monsterPrefab);
                        krake = monsterVRGameObject.GetComponent<VREnemyKrake>();
                        break;
                    case 1: //Ship
                        shipVRGameObject = (GameObject)Instantiate(enemyShipPrefab);
                        enemyShip = shipVRGameObject.GetComponent<VREnemyShip>();
                        break;
                    default:
                        break;

                }

                actionIcon_crosshair_go = (GameObject)Instantiate(actionIcon_crosshair_prefab);
                ActionIcon actionIcon_crosshair = actionIcon_crosshair_go.GetComponent<ActionIcon>();
                actionIcon_crosshair.setType(ActionIcon.ActionIconType.Crosshair);
                actionIcon_crosshair.setVrHandler(this.gameObject);

                alreadydead = false;
                break;

            case 3: //Insel

                switch (this.island)
                {
                    default:
                    case 0:
                        Debug.Log("Initiate VR Island");
                        GameObject islandVRGameObject = (GameObject)Instantiate(islandPrefab0);

                        break;
                    case 1:
                    case 2:
                        break;
                    
                }
                actionIcon_shovel_go = (GameObject)Instantiate(actionIcon_shovel_prefab);
                ActionIcon actionIcon_shovel = actionIcon_shovel_go.GetComponent<ActionIcon>();
                actionIcon_shovel.setType(ActionIcon.ActionIconType.Shovel);
                actionIcon_shovel.setVrHandler(this.gameObject);

                break;

            default: break;
        }


        allowedToLeave = false;
    }
    public void actionIconEvent(ActionIcon.ActionIconType type) {
        if (type == ActionIcon.ActionIconType.Crosshair)
        {
            this.switchToCannons();
        }
        else if (type == ActionIcon.ActionIconType.Steeringwheel)
        {
            this.switchToMap();
        }
        else if (type == ActionIcon.ActionIconType.Shovel)
        {
            this.goToDigging();
        }

        this.setActionIconsVisible(type);
    }
    private void setActionIconsVisible(ActionIcon.ActionIconType type) {

        actionIcons = new GameObject[] { actionIcon_crosshair_go, actionIcon_steeringwheel_go, actionIcon_shovel_go };
        foreach (GameObject icon in actionIcons)
        {
            if (icon != null)
            {
                if (icon.GetComponent<ActionIcon>().type == type) {
                    icon.SetActive(false);
                }
                else
                {
                    icon.SetActive(true);
                }               
            }

        }
    }
    private void switchToCannons() {
        // Set position and rotation of camera
        cameraRig.transform.position = new Vector3(-1.79f, -2.553f, -1.459f);
        Vector3 cameraRigEulerRotation = new Vector3(0, 90, 0);
        cameraRig.transform.rotation = Quaternion.Euler(cameraRigEulerRotation);
        this.mode = 0;
    }
    private void switchToMap()
    {
        // Set position and rotation of camera
        cameraRig.transform.position = new Vector3(-1.9f, 0.03f, -11.59f);
        Vector3 cameraRigEulerRotation = new Vector3(0, 90, 0);
        cameraRig.transform.rotation = Quaternion.Euler(cameraRigEulerRotation);
        this.mode = 1;
    }
    private void goToDigging()
    {
        cameraRig.transform.position = new Vector3(36.5f, 0f, -10.72f);
        Vector3 cameraRigEulerRotation = new Vector3(0, 90, 0);
        cameraRig.transform.rotation = Quaternion.Euler(cameraRigEulerRotation);
        this.mode = 4;
    }

    void initLooting() {
        this.mode = 2;
        Debug.Log("Mode switched to Looting");

        if (krake != null)
        {
            Destroy(krake.gameObject);
            krake = null;
        }
        if (enemyShip != null)
        {
            Destroy(enemyShip.gameObject);
            enemyShip = null;
        }
        

        actionIcon_steeringwheel_go = (GameObject)Instantiate(actionIcon_steeringwheel_prefab);
        ActionIcon actionIcon_steeringwheel = actionIcon_steeringwheel_go.GetComponent<ActionIcon>();
        actionIcon_steeringwheel.setType(ActionIcon.ActionIconType.Steeringwheel);
        actionIcon_steeringwheel.setVrHandler(this.gameObject);


        allowedToLeave = true;
        // Set position and rotation of camera
        // cameraRig.transform.position = new Vector3(-3.56f, 0.59f, -13.42f);
        // Vector3 cameraRigEulerRotation = new Vector3(0, 180, 0);
        // cameraRig.transform.rotation = Quaternion.Euler(cameraRigEulerRotation);

        //Invoke("endVR", 15);
        this.spawnLoot();
    }

    void spawnLoot() {
        int rndNum = (int)Random.Range(4, 10);
        for (int i = 0; i < rndNum; i++)
        {
            //x : zwischen 9 und 15
            //y : -6
            //z : zwischen -7 und 7
            Vector3 rndPosition = new Vector3(Random.value * 6 + 9, -6, Random.value * 14 - 7);
            GameObject lootCrateObject = (GameObject)Instantiate(lootCratePrefab);
            // LootCrate lootCrateScript = lootCrateObject.GetComponent<LootCrate>();
            lootCrateObject.gameObject.transform.position = rndPosition;
        }
    }

	// Update is called once per frame
	void Update () {

        if (alreadydead == false)
        {
            if ((krake != null && krake.dead == true) || (enemyShip != null && enemyShip.dead == true))
            {
                Invoke("initLooting", 3);
                alreadydead = true;
            }
            
        }
     

	}

    public void endVR()
    {
        if (allowedToLeave)
        {
            GameObject[] leftover_lootcrates = GameObject.FindGameObjectsWithTag("LootCrate");

            foreach (GameObject lootcrate in leftover_lootcrates)
            {
                if (lootcrate != null)
                {
                    Destroy(lootcrate);
                }
               
            }/*
            foreach (GameObject icon in actionIcons)
            {
                if (icon != null)
                {
                    Destroy(icon);
                }
            }*/
            PlayerController.Instance.EndVRMode();
        }
        
    }
    /*
    private static VRhandler instance;
    public static VRhandler Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                instance = (VRhandler)obj.AddComponent(typeof(VRhandler));
            }
            return instance;
        }
    }*/
}
