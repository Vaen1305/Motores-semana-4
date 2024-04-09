using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    [SerializeField] private Transform initialPosition; 
    [SerializeField] private float moveSpeed = 5f; 
    [SerializeField] private float returnSpeed = 2f; 
    [SerializeField] private CircleCollider2D detectionArea; 
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject projectilPrefrab;
    [SerializeField] private Transform fire;
    [SerializeField] private GameObject player; 



    private bool playerDetected = false;
    private Vector3 playerLastPosition;
    private float shootInterval = 2f; 
    private float shootTimer = 0f;

    private void Update()
    {
        if (playerDetected)
        {
            playerLastPosition = player.transform.position;

            MoveTowardsPlayer();
            UpdateShootTimer();
        }
        else
        {
            ReturnToInitialPosition();
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerLastPosition, moveSpeed * Time.deltaTime);
    }

    private void ReturnToInitialPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, initialPosition.position, returnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
            playerLastPosition = other.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            playerLastPosition = Vector3.zero;
        }
    }
    private void UpdateShootTimer()
    {
        if (shootTimer <= 0f)
        {
            ShootProjectile();
            shootTimer = shootInterval; 
        }
        else
        {
            shootTimer -= Time.deltaTime; 
        }
    }
    private void ShootProjectile()
    {
        if (fire != null && projectilPrefrab != null)
        {
            Vector3 direction = playerLastPosition - fire.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Instantiate(projectilPrefrab, fire.position, rotation);
        }
    }

}