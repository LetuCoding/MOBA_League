using ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "MOBA/Champion Data")]
public class ChampionData : ScriptableObject
{
    [Header("Info")]
    public string championName;

    [Header("Health")]
    public float maxHealth;
    public float healthRegen;

    [Header("Resource")]
    public ResourceType resourceType;
    public float maxResource;
    public float resourceRegen;

    [Header("Resistances")]
    public float armor;
    public float magicArmor;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Combat")]
    public float attackDamage;
    public float attackSpeed;

    [Header("Spells")]
    public SpellData spellQ;
    public SpellData spellW;
    public SpellData spellE;
    public SpellData spellR;
}