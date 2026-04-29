using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions actions;

    private PlayerMovement movement;
    private SpellController spellController;
    private ChampionStats stats;

    void Awake()
    {
        actions         = new InputSystem_Actions();
        movement        = GetComponent<PlayerMovement>();
        spellController = GetComponent<SpellController>();
        stats           = GetComponent<ChampionStats>();
    }

    void OnEnable()  => actions.Enable();
    void OnDisable() => actions.Disable();

    void Update()
    {
        HandleMovementInput();
        HandleSpellInput();
    }

    private void HandleMovementInput()
    {
        if (actions.UI.RightClick.WasPressedThisFrame())
            movement.TryMoveTo(Mouse.current.position.ReadValue());
    }

    private void HandleSpellInput()
    {
        if (actions.Player.Skill1.WasPressedThisFrame())
            spellController.TryCast(stats.championData.spellQ, Mouse.current.position.ReadValue());

        /*if (actions.Player.Skill2.WasPressedThisFrame())
            spellController.TryCast(stats.championData.spellW, Mouse.current.position.ReadValue());

        if (actions.Player.Skill3.WasPressedThisFrame())
            spellController.TryCast(stats.championData.spellE, Mouse.current.position.ReadValue());

        if (actions.Player.Skill4.WasPressedThisFrame())
            spellController.TryCast(stats.championData.spellR, Mouse.current.position.ReadValue());
            */
    }
}