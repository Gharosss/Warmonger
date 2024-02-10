using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int unitSide = 1;
    private Vector3 spawnOffset;
    private Vector3 position;
    public float maxHealth;
    public float health;
    public float money = 0;
    public float manPower = 0;
    private float income = 2;
    public float incomeBoost;
    private float manPowerGain = 2;
    public float manPowerGainBoost;
    private float recruitmentProgress = 0;
    public float recruitmentPercentage = 0;
    public bool recruitingUnit = false;
    private GameObject spawningUnit;
    public GameObject enemyBase;
    [SerializeField] private Slider healthBar;

    public List<GameObject> SpawnedUnits;
    void Start()
    {
        SpawnedUnits = new List<GameObject>();

        incomeBoost = 1;
        manPowerGainBoost = 1;
        maxHealth = 1000;
        health = maxHealth;
        spawnOffset = new Vector3(unitSide * 3, -2, 0);
        position = transform.position;
        if (unitSide == -1) {  //makes the enemy units red
            transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().color = Color.red;
        }
       
    }
    

    // Update is called once per frame
    void Update()
    {
        money += income * Time.deltaTime * incomeBoost;
        manPower += manPowerGain * Time.deltaTime * manPowerGainBoost;
        healthBar.value = health / maxHealth;
        if (recruitingUnit) {
            if (recruitmentProgress < spawningUnit.GetComponent<UnitBehavior>().data.recruitmentTime) {
                recruitmentProgress += Time.deltaTime;
                recruitmentPercentage = recruitmentProgress / spawningUnit.GetComponent<UnitBehavior>().data.recruitmentTime;
            }
            else {
                recruitmentProgress = 0;
                recruitingUnit = false;
                Spawning(spawningUnit);
            }
        }
    }
    public void ToSpawn(GameObject unit) {
        if(!recruitingUnit && money > unit.GetComponent<UnitBehavior>().data.cost && manPower > unit.GetComponent<UnitBehavior>().data.manUsage) {
            money -= unit.GetComponent<UnitBehavior>().data.cost;
            manPower -= unit.GetComponent<UnitBehavior>().data.manUsage;
            recruitingUnit = true;
            spawningUnit = unit;
            Debug.Log("Spawning " + spawningUnit.GetComponent<UnitBehavior>().data.name);
        }
    }

    private void Spawning(GameObject unit) {
        GameObject newUnit = (GameObject)Instantiate(unit, position + spawnOffset, new Quaternion());
        newUnit.GetComponent<UnitBehavior>().homeBase = this;
        SpawnedUnits.Add(newUnit);
        spawningUnit = null;
        string objectNames = string.Join(", ", SpawnedUnits.ConvertAll(obj => obj.GetComponent<UnitBehavior>().data.name));
        Debug.Log("Army of the base " + unitSide + " consists of " + objectNames);
    }
}
