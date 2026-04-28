using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
   private RaycastHit hit;
   private InputSystem_Actions actions;
   public Camera cam;
   private Vector3 target;
   private NavMeshAgent  agent;
   Animator animator;
  
   private bool isCasting;
   public bool isMoving {private set; get;}


   public ChampionData championData;
   
   
   
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      
       actions = new InputSystem_Actions();
       agent = GetComponent<NavMeshAgent>();
       animator = GetComponent<Animator>();
       actions.Enable();
      
      
      
       agent.speed = 6f;            
       agent.acceleration = 1000f;  
       agent.angularSpeed = 9999f;  
       agent.autoBraking = false;   
       agent.stoppingDistance = 0f; 
       agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
   }


  
   // Update is called once per frame
   void Update()
   {
       
       if (actions.Player.Skill1.WasPressedThisFrame() && !isCasting)
       {
           CastSpell(championData.spellQ);
       }


       if (!isCasting)
       {
           
        HandleMovement();
      
       }
       
       // Movimiento + animación
       isMoving = agent.velocity.sqrMagnitude > 0.01f;
       animator.SetBool("isMoving", isMoving);
   }



   public void CastSpell(SpellData spell)
   {
       if (isCasting) return;
       StartCoroutine(Cast(spell));
   }

   IEnumerator Cast(SpellData spell)
   {
       isCasting = true;

       agent.ResetPath();

       // 🔽 Obtener dirección del mouse
       Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

       if (Physics.Raycast(ray, out hit))
       {
           Vector3 direction = (hit.point - transform.position);
           direction.y = 0;

           if (direction != Vector3.zero)
           {
               transform.rotation = Quaternion.LookRotation(direction);
           }
       }

       animator.SetTrigger(spell.spellName);

       if (spell.spellName == "Steel Tempest")
       {
           animator.speed = 1 + championData.attackSpeed;
       }
       
       float finalCastTime = spell.castTime / championData.attackSpeed;
       yield return new WaitForSeconds(finalCastTime);
       animator.speed = 1;
       isCasting = false;
   }

   private void HandleMovement()
   {
       if (actions.UI.RightClick.WasPressedThisFrame())
       {
           Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());


           if (Physics.Raycast(ray, out hit))
           {
               if (agent.isOnNavMesh)
               {
                   if (Vector3.Distance(transform.position, hit.point) > 0.1f)
                   {
                       agent.ResetPath();
                       agent.SetDestination(hit.point);
                   }
               }
           }
       }
      
   }


}
