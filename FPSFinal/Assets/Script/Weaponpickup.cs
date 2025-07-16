using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab; // ��ҳ����õ�Prefab

    public void OnPickUp()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerWeaponManager>().PickupWeapon(weaponPrefab);
            Debug.Log("������ˣ�" + weaponPrefab.name);
            Destroy(gameObject);
        }
    }
}

