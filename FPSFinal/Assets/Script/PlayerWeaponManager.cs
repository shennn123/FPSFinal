using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Transform weaponHolder;

    private GameObject currentWeapon;

    public void PickupWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(weaponPrefab, weaponHolder);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}