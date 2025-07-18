using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AmmoController : MonoBehaviour
{
    public static AmmoController instance; // Singleton instance of AmmoController
    public int MaxAmmo = 60; // Maximum ammo the player can hold
    public int currentAmmo = 30; // Current ammo the player has
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
        //UIController.instance.ammoText.text = "Ammo: " + currentAmmo + "/" + MaxAmmo; // Set the initial health text
    }

    // Update is called once per frame
    void Update()
    {

        // UIController.instance.ammoText.text = "Ammo: " + currentAmmo + "/" + MaxAmmo; // Set the initial health text
    }

    public void Reload()
    {
        if (currentAmmo < MaxAmmo)
        {
            currentAmmo = MaxAmmo; // Reload to maximum ammo
            //UIController.instance.ammoText.text = "Ammo: " + currentAmmo + "/" + MaxAmmo; // Update the ammo text
            Debug.Log("Reloaded to max ammo: " + currentAmmo);
        }
        else
        {
            Debug.Log("Already at max ammo: " + currentAmmo);
        }
    }
}
