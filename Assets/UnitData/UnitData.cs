using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData", order = 1)]

public class UnitData : ScriptableObject {
    public string unitName;
    public float cost;
    public float manUsage;
    public float maxHealth;
    public float speed;
    public float damage;
    public float attackSpeed;
    public float range;
    public float blockChance;
    public float recruitmentTime;
    public int unitClass; //1 for Light Melee, 2 for Heavy Melee, 3 for Support
}
