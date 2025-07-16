using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab; // 玩家持有用的Prefab

    public void OnPickUp()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerWeaponManager>().PickupWeapon(weaponPrefab);
            Debug.Log("你捡起了：" + weaponPrefab.name);
            Destroy(gameObject);
        }
    }
}

