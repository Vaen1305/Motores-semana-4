using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float chaseVelocityModifier = 10f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float rayDistance = 5f; 
    [SerializeField] private Color rayColor = Color.red; 

    private Transform currentPositionTarget;
    private int patrolPos = 0;

    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update() {
        CheckNewPoint();

        if (DetectPlayer())
        {
            animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * chaseVelocityModifier;
        }
        else
        {
            animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
        }
    }

    private void CheckNewPoint(){
        if(Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25){
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length? 0: patrolPos+1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }
        
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    private bool DetectPlayer()
    {
        Vector2 direction = currentPositionTarget.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, playerLayer);
        Debug.DrawRay(transform.position, direction.normalized * rayDistance, rayColor);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }
}
