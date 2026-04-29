using UnityEngine;

[CreateAssetMenu(fileName = "SpellData", menuName = "Scriptable Objects/SpellData")]
public class SpellData : ScriptableObject
{
    public string spellName;
    public float castTime;
    public float cooldown;

    [Tooltip("Si es true, el cast time se divide por el attack speed del campeón")]
    public bool scalesWithAttackSpeed;
}