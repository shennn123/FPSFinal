using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Gun[] allGuns; // 所有枪Prefab
    public PlayerController playerController;

    public void PickUpGunToReplaceCurrent(int allGunIndex)
    {
        Gun pickedGun = allGuns[allGunIndex];

        int replaceIndex = playerController.currentGunIndex;

        // 关闭旧枪
        playerController.guns[replaceIndex].gameObject.SetActive(false);

        // 替换
        Gun newGun = Instantiate(pickedGun); // 先实例化
        newGun.transform.SetParent(playerController.gunHolder1); // 关键：设定父物体为 gunHolder
        newGun.transform.localPosition = Vector3.zero;
        newGun.transform.localRotation = Quaternion.identity;

        // 3. 更新引用
        playerController.guns[replaceIndex] = newGun;
        playerController.activeGun = newGun;

        Debug.Log("已更换为新武器：" + newGun.name);
    }
}