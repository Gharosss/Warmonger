using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBehavior : MonoBehaviour {
    public bool inContact = false; //In contact with a unit infront of it
    public BaseScript homeBase;
    public List<GameObject> healers;
    public GameObject inRangeEnemy;

    public float health;
    [SerializeField]private Slider healthBar;
    protected float minRNG = 1f;
    protected float maxRNG = 2.5f;
    public UnitData data;
    private bool currentlyAttacking = false;

    void Start() {
        health = data.maxHealth;
        healers = new List<GameObject>();
        if (homeBase.unitSide == -1) {  //Changes the enemy units
            GetComponent<SpriteRenderer>().color = new Color (170, 0, 0); 
            transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().color = Color.red;
            gameObject.layer = 17;
        }

    }

    void Update() {
        if(!currentlyAttacking) {
            CheckRange();
        }
        healthBar.value = health / data.maxHealth; // Healthbar filling
        if (health <= 0) {
            Object.Destroy(this.gameObject);
        }
    }
    private void OnDestroy() {
        homeBase.SpawnedUnits.Remove(this.gameObject);
        foreach(GameObject healer in healers) { // clear the ally list of any object healing this one
            healer.GetComponent<HealerScript>().inRangeAllies.Remove(this.gameObject);
        }
        Debug.Log("me ded"); // me ded
    }

    private void FixedUpdate() {
        if (!inContact) transform.position = transform.position + new Vector3(data.speed * Time.deltaTime * homeBase.unitSide, 0); // moves the unit if not in contact
        transform.position = transform.position + new Vector3(0, -transform.position.y); // Keeps objects on the ground
        transform.rotation = Quaternion.Euler(0,0,0); // Keeps objects upright

    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.layer == 14 || collision.gameObject.layer == 16) { // Checks if in contact with enemy base
            inContact = true;
        }
        else if (homeBase.unitSide == 1) { //if this unit is on the left side
            if (collision.gameObject.transform.position.x > transform.position.x) {
                inContact = true; 
            }
        }
        else if (collision.gameObject.transform.position.x < transform.position.x) { //if this unit is on the right side
            inContact = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        inContact = false;
    }


    private void CheckRange() { //Attacks the closest enemy if in range

        if(homeBase.enemyBase.GetComponent<BaseScript>().SpawnedUnits.Count >= 1) {
            if (transform.lossyScale.x /2 + data.range + Closest(homeBase.enemyBase.GetComponent<BaseScript>().SpawnedUnits).transform.lossyScale.x/2>= Vector3.Distance(transform.position, Closest(homeBase.enemyBase.GetComponent<BaseScript>().SpawnedUnits).transform.position) && !currentlyAttacking) { // Range check for the closest enemy unit
                StartCoroutine(SleepCoroutine(Closest(homeBase.enemyBase.GetComponent<BaseScript>().SpawnedUnits), 10 / data.attackSpeed)); // Attack
                // Debug.Log(this.gameObject + " is attacking " + homeBase.enemyBase.GetComponent<BaseScript>().SpawnedUnits[0]);
            }
        }
        else if (transform.lossyScale.x / 2 + data.range + homeBase.enemyBase.transform.lossyScale.x /2 >= Vector3.Distance(transform.position, homeBase.enemyBase.transform.position) && !currentlyAttacking) { // Range check for the enemy base
                StartCoroutine(SleepCoroutine(homeBase.enemyBase, 10 / data.attackSpeed)); // Attack
                // Debug.Log(this.gameObject + " is attacking " + homeBase.enemyBase);
        }
        
    }
    private void Attack(GameObject victim) { //Damage phase of an attack
        
        if (victim.TryGetComponent(out UnitBehavior enemy)) {
            if(enemy.data.blockChance < Random.Range(1, 101)) {
                enemy.health -= data.damage * Random.Range(minRNG, maxRNG) * 5;
            }
        }
        else victim.GetComponent<BaseScript>().health -= data.damage * Random.Range(minRNG, maxRNG) * 5;
        // Debug.Log(this.gameObject.name + " is attacking to " + victim);

    }
    private IEnumerator SleepCoroutine(GameObject victim, float attackTime) { //Initial phase of an attack with a cooldown
        Attack(victim);
        currentlyAttacking = true;
        yield return new WaitForSeconds(attackTime);
        currentlyAttacking = false;
    }

    private GameObject Closest(List<GameObject> list) { //Finds the closest object in the given list to this object
        GameObject closest = list[0];
        foreach (GameObject unit in list) { 
            if(Vector3.Distance(this.gameObject.transform.position, unit.transform.position) < Vector3.Distance(this.gameObject.transform.position, closest.transform.position)) {
                closest = unit;
            }
        }
        return closest;
    }
}
