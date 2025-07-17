using UnityEngine;

public class WeaponPickupTrigger : MonoBehaviour
{
    public int gunIndexInAllGuns;
    public PlayerWeaponManager playerWeaponManager;

    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            playerWeaponManager.PickUpGunToReplaceCurrent(gunIndexInAllGuns);
            Destroy(transform.parent.gameObject); // 销毁整个枪（父物体）
        }
    }
}