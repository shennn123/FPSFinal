using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bulletPrefab; // Prefab for the bullet

    public bool canAutoFire = false; // Whether the gun can auto-fire

    public float firerate = 0.5f; // Rate of fire in seconds

    [HideInInspector]
    public float fireCounter; // Counter to track time since last fire


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime; // Decrease the fire counter by the time since last frame
        }
    }
}
