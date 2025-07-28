using UnityEngine;

public class Game1UI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Show("InGameUI");
        UIManager.Show("Map");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
