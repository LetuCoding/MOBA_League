using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;

    private NavMeshAgent agent;
    private Animator animator;
    private ChampionStats stats;

    public bool IsMoving { get; private set; }
    public bool IsCasting { get; private set; }  // lo escribe SpellController

    void Awake()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stats    = GetComponent<ChampionStats>();

        agent.acceleration          = 1000f;
        agent.angularSpeed          = 9999f;
        agent.autoBraking           = false;
        agent.stoppingDistance      = 0f;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    void Update()
    {
        agent.speed = stats.totalMoveSpeed;

        IsMoving = agent.velocity.sqrMagnitude > 0.01f;
        animator.SetBool("isMoving", IsMoving);
    }

    // Llamado por PlayerInput
    public void TryMoveTo(Vector2 screenPosition)
    {
        if (IsCasting) return;

        Ray ray = cam.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && agent.isOnNavMesh)
        {
            if (Vector3.Distance(transform.position, hit.point) > 0.1f)
            {
                agent.ResetPath();
                agent.SetDestination(hit.point);
            }
        }
    }

    public void StopMovement() => agent.ResetPath();

    // Llamado por SpellController para bloquear el movimiento durante el cast
    public void SetCasting(bool value) => IsCasting = value;

    // Rota el personaje hacia un punto en pantalla
    public void FaceScreenPoint(Vector2 screenPosition)
    {
        Ray ray = cam.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}