using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
    // Start is called before the first frame update
    private BaseScript baseScript;
    public GameObject levy;
    public GameObject knight;
    public GameObject healer;
    public GameObject archer;
    public GameObject heavyInfantry;
    public bool hasUnits = false;
    public GameObject lastUnit;
    public UnitBehavior lastUnitBehaviour;
    private int planNum;
    private int moneyDifference;
    private int menDifference;
    private int numberOfHeavies = 0;
    private bool heavyLowOnHP;
    private int numberOfHealers = 0;
    void Start()
    {
        moneyDifference = 10;
        menDifference = 10;
        baseScript = GetComponent<BaseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        numberOfHeavies = 0;
        numberOfHealers = 0;
        if (baseScript.SpawnedUnits.Count > 0) {
            hasUnits = true;
            lastUnit = baseScript.SpawnedUnits[baseScript.SpawnedUnits.Count - 1];
            lastUnitBehaviour = lastUnit.GetComponent<UnitBehavior>();
            foreach (GameObject unit in baseScript.SpawnedUnits) { // Counting Units
                if (unit.GetComponent<UnitBehavior>().data.unitClass == 2) { // Counting Heavies
                    numberOfHeavies++;
                    if(unit.GetComponent<UnitBehavior>().health < unit.GetComponent<UnitBehavior>().data.maxHealth / 3) {
                        heavyLowOnHP = true;
                    }
                    else heavyLowOnHP = false;
                }
                if (unit.GetComponent<UnitBehavior>().data.name == "Healer") { // Counting Healers
                    numberOfHealers++;
                }
            }
        }
        else {
            hasUnits = false; 
        }


        switch(planNum) {
            case 0:
                Decide();   
                break;
            case 1: // Spawn levy
                if(hasUnits) { 
                    if(numberOfHeavies > 0 && lastUnitBehaviour.data.unitClass != 1) {
                        Debug.Log("Better units in the army");
                        planNum = 0;
                        break;
                    }
                }
                if(baseScript.money > heavyInfantry.GetComponent<UnitBehavior>().data.cost && baseScript.manPower < 20) {
                    Debug.Log("Too much money while low on men");
                    planNum = 0;
                    break;
                }
                else if (baseScript.money > levy.GetComponent<UnitBehavior>().data.cost && baseScript.manPower > levy.GetComponent<UnitBehavior>().data.manUsage) {
                    baseScript.ToSpawn(levy);
                    planNum = 0;
                }
                if(baseScript.manPower > 30) planNum = 1;
                break;
            case 2: // Spawn Knight
                if (baseScript.money < 30) {
                    Debug.Log("Not sufficient funds");
                    planNum = 0;
                    break;
                }
                if (hasUnits) {
                    if (numberOfHeavies > 0) {
                        Debug.Log("Too many Heavies already");
                        planNum = 0;
                        break;
                    }
                }
                if (baseScript.money > knight.GetComponent<UnitBehavior>().data.cost && baseScript.manPower > knight.GetComponent<UnitBehavior>().data.manUsage) {
                    baseScript.ToSpawn(knight);
                    planNum = 0;
                }
                break;
            case 3: // Spawn Healer
                if(hasUnits) {
                    if (numberOfHeavies == 0 || numberOfHealers > 2 || heavyLowOnHP) {
                        Debug.Log("Needs a Knight to heal, there are too many healers or heavy is about to die");
                        planNum = 0;
                        break;
                    }
                    else if (baseScript.money > healer.GetComponent<UnitBehavior>().data.cost && baseScript.manPower > healer.GetComponent<UnitBehavior>().data.manUsage) {
                        baseScript.ToSpawn(healer);
                        planNum = 0;
                    }
                    if(numberOfHealers < 2) { 
                        planNum = 3;
                    }
                }
                else {
                    Debug.Log("Want a Knight to heal");
                    planNum = 0;
                }
                break;
            case 4: // Spawn Archer

                if (hasUnits) {
                    if (lastUnitBehaviour.data.unitClass != 3 || heavyLowOnHP) {
                        Debug.Log("Needs to be behind another support or heavy is about to die");
                        planNum = 0;
                        break;
                    }
                    else if (baseScript.money > archer.GetComponent<UnitBehavior>().data.cost && baseScript.manPower > archer.GetComponent<UnitBehavior>().data.manUsage) {
                        baseScript.ToSpawn(archer);
                        planNum = 0;
                    }
                }
                else {
                    Debug.Log("Not sufficient units");
                    planNum = 0;
                }
                break;
            case 5: // Spawn Heavy Infantry

                if (baseScript.money < 30 && baseScript.manPower < heavyInfantry.GetComponent<UnitBehavior>().data.manUsage - menDifference) {
                    Debug.Log("Not sufficient funds");
                    planNum = 0;
                    break;
                }
                if (numberOfHeavies == 0 && baseScript.manPower > 50) {
                    Debug.Log("Using up manpower instead");
                    planNum = 1;
                    break;
                }
                if(hasUnits) {
                    if(numberOfHeavies > 0) {
                        Debug.Log("Too many Heavies already");
                        planNum = 0;
                        break;
                    }
                }
                
                if (baseScript.money > heavyInfantry.GetComponent<UnitBehavior>().data.cost && baseScript.manPower > heavyInfantry.GetComponent<UnitBehavior>().data.manUsage) {
                    baseScript.ToSpawn(heavyInfantry);
                    planNum = 0;
                }
                break;
        }
    }


    void Decide() {
        planNum = Random.Range(1, 6);
        Debug.Log("Chosen tactic is " +planNum);
        /*
        if(baseScript.SpawnedUnits.Count > 0) {
            Debug.Log("Last unit is " + baseScript.SpawnedUnits[baseScript.SpawnedUnits.Count - 1]);
        }*/
    }
    
}

/* 
hasCombatant = false;
foreach (GameObject obj in baseScript.SpawnedUnits) {
    if (obj != healer) {
        hasCombatant = true;
    }
}
if (!hasCombatant) {
    Debug.Log("No combatants");
    planNum = 0;
}
*/