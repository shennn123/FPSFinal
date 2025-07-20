using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    public float moveSpeed = 20f; // Speed of the bullet
    private Rigidbody rb; // Reference to the Rigidbody component
    public float lifeTime = 2f; // Time before the bullet is destroyed  
    public GameObject laserImpct; // Reference to the laser impact effect
    public int Damage = 10; // Damage dealt by the bullet   

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
  
        if (other.CompareTag("Monster"))
        {
            other.gameObject.GetComponent<MonsterHealth>()?.TakeDamage(10); // Call TakeDamage on the MonsterController if it exists
        }
    }


}
