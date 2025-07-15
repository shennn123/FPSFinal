using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance; // Singleton instance of UIController
    public Slider healthSlider; // Reference to the health slider UI element 
    public Text healthText; // Reference to the health text UI element
    public Text ammoText; // Reference to the ammo text UI element



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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
