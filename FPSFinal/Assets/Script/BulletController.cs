using System.Runtime.CompilerServices;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    public float moveSpeed = 20f; // Speed of the bullet
    private Rigidbody rb; // Reference to the Rigidbody component
    public float lifeTime = 2f; // Time before the bullet is destroyed  
    public GameObject laserImpct; // Reference to the laser impact effect
    public int Damage = 1; // Damage dealt by the bullet   

    //public bool dmageEnemy = false; // Flag to check if the bullet should damage the enemy
    //public bool dmagePlayer = false;
    public bool attactplayer = false; // Flag to check if the bullet should damage the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the bullet   
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.forward * moveSpeed; // Set the bullet's velocity to move forward

        lifeTime -= Time.deltaTime; // Decrease the lifetime of the bullet  
        if (lifeTime <= 0f)
        {
            Destroy(gameObject); // Destroy the bullet after its lifetime expires
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        IDamageable damageable = other.GetComponent<IDamageable>(); // Get the IDamageable component from the collided object
        if (damageable != null) // Check if the collided object has an IDamageable component
        {
            damageable.TakeDamage(Damage, attactplayer); // Call the TakeDamage method on the IDamageable component with the bullet's damage and attack player flag
        }


        float offset = 0.7f;
        Vector3 newPosition = transform.position + transform.forward * offset; // Calculate the new position for the laser impact effect
        Instantiate(laserImpct, transform.position, transform.rotation); // Instantiate the laser impact effect at the bullet's position and rotation
        Destroy(gameObject); // Destroy the bullet when it collides with another object
    }

}
