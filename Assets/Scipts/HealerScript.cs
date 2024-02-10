using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealerScript : MonoBehaviour
{
    private UnitBehavior unitBehavior;
    private bool currentlyHealing = false;
    private float healFrequency = 2;
    private float healStrength = 5;
    private float minRNG = 1;
    private float maxRNG = 2;
    public List<GameObject> inRangeAllies;

    void Start() {
        inRangeAllies = new List<GameObject>();
        unitBehavior = GetComponent<UnitBehavior>();
    }

    void Update()
    {
        foreach (GameObject ally in inRangeAllies) {
            if (ally.GetComponent<UnitBehavior>().health < ally.GetComponent<UnitBehavior>().data.maxHealth && !currentlyHealing) {
                StartCoroutine(SleepCoroutine(ally, 1 / healFrequency));

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.TryGetComponent(out UnitBehavior inRangeUnitBehavior)) { // if in contact with an ally
            if (inRangeUnitBehavior.homeBase.unitSide == unitBehavior.homeBase.unitSide && !inRangeAllies.Contains(trigger.gameObject)) {
                Debug.Log(trigger.gameObject + " has been added to inRangeAllies");
                inRangeAllies.Add(trigger.gameObject);
                inRangeUnitBehavior.healers.Add(this.gameObject);
            }
        }
    }
    private IEnumerator SleepCoroutine(GameObject ally, float attackSpeed) {
        Heal(ally);
        currentlyHealing = true;
        yield return new WaitForSeconds(attackSpeed);
        currentlyHealing = false;
    }
    void Heal(GameObject ally) {
        // Debug.Log("Healing " + ally);
            ally.GetComponent<UnitBehavior>().health += healStrength * Random.Range(minRNG, maxRNG); // Heals the ally first
            if(ally.GetComponent<UnitBehavior>().health> ally.GetComponent<UnitBehavior>().data.maxHealth) { // Checks if overhealed
            ally.GetComponent<UnitBehavior>().health = ally.GetComponent<UnitBehavior>().data.maxHealth; // Sets the health to max health

                }
            }
}
