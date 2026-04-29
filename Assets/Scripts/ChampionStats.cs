using ScriptableObjects;
using UnityEngine;

public class ChampionStats : MonoBehaviour
{
    public ChampionData championData;

    [Header("Health")]
    public float totalHealth        { get; private set; }
    public float totalHealthRegen   { get; private set; }

    [Header("Resistances")]
    public float totalArmor         { get; private set; }
    public float totalMagicArmor    { get; private set; }

    [Header("Resource")]
    public float totalResource      { get; private set; }
    public float totalResourceRegen { get; private set; }

    [Header("Movement")]
    public float totalMoveSpeed     { get; private set; }

    [Header("Combat")]
    public float totalAttackDamage  { get; private set; }
    public float totalAttackSpeed   { get; private set; }

    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        inventory.OnInventoryChanged += RecalculateStats;

        RecalculateStats();
    }

    void OnDestroy()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= RecalculateStats;
    }

    private void ResetToBase()
    {
        totalHealth        = championData.maxHealth;
        totalHealthRegen   = championData.healthRegen;
        totalArmor         = championData.armor;
        totalMagicArmor    = championData.magicArmor;
        totalResource      = championData.maxResource;
        totalResourceRegen = championData.resourceRegen;
        totalMoveSpeed     = championData.moveSpeed;
        totalAttackDamage  = championData.attackDamage;
        totalAttackSpeed   = championData.attackSpeed;
    }

    public void RecalculateStats()
    {
        ResetToBase();

        foreach (var item in inventory.Items)
        {
            totalHealth        += item.health;
            totalHealthRegen   += item.healthRegen;
            totalArmor         += item.armor;
            totalMagicArmor    += item.magicArmor;
            totalMoveSpeed     += item.moveSpeed;
            totalAttackDamage  += item.attackDamage;
            totalAttackSpeed   += item.attackSpeed;

            // Solo suma resource si el tipo del campeón lo usa
            if (championData.resourceType != ResourceType.None)
            {
                totalResource      += item.resource;
                totalResourceRegen += item.resourceRegen;
            }
        }
    }
}