using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AmmoController : MonoBehaviour
{
    public static AmmoController instance; // Singleton instance of AmmoController
    public int MaxAmmo = 30; // Maximum ammo the player can hold
    public int currentAmmo; // Current ammo the player has
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign the singleton instance
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    void Start()
    {
        currentAmmo = MaxAmmo; // Initialize current ammo to maximum ammo
        //UIController.instance.ammoText.text = "Ammo: " + currentAmmo + "/" + MaxAmmo; // Set the initial health text
    }

    // Update is called once per frame
    void Update()
    {
       // UIController.instance.ammoText.text = "Ammo: " + currentAmmo + "/" + MaxAmmo; // Set the initial health text
    }
}
