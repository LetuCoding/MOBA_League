using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int itemPrice;

    [Header("Health")]
    public float health;
    public float healthRegen;

    [Header("Resistances")]
    public float armor;
    public float magicArmor;

    [Header("Resource")]
    public float resource;
    public float resourceRegen;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Combat")]
    public float attackDamage;
    public float attackSpeed;
}