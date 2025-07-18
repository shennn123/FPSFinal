using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //PlayerHealth player = other.GetComponent<PlayerHealth>();
            //if (player != null)
                //player.TakeDamage(damage);
        }
    }
}

