using System.Collections;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    private Animator animator;
    private ChampionStats stats;
    private PlayerMovement movement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        stats    = GetComponent<ChampionStats>();
        movement = GetComponent<PlayerMovement>();
    }

    // Llamado por PlayerInput
    public void TryCast(SpellData spell, Vector2 mouseScreenPosition)
    {
        if (spell == null)        return;
        if (movement.IsCasting)   return;

        StartCoroutine(CastRoutine(spell, mouseScreenPosition));
    }

    private IEnumerator CastRoutine(SpellData spell, Vector2 mouseScreenPosition)
    {
        movement.StopMovement();
        movement.SetCasting(true);
        movement.FaceScreenPoint(mouseScreenPosition);

        animator.SetTrigger(spell.spellName);

        float finalCastTime = spell.scalesWithAttackSpeed
            ? spell.castTime / stats.totalAttackSpeed
            : spell.castTime;

        if (spell.scalesWithAttackSpeed)
            animator.speed = stats.totalAttackSpeed;

        yield return new WaitForSeconds(finalCastTime);

        animator.speed = 1f;
        movement.SetCasting(false);
    }
}