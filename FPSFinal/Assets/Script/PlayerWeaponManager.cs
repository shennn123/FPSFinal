using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Gun[] allGuns; // ����ǹPrefab
    public PlayerController playerController;

    public void PickUpGunToReplaceCurrent(int allGunIndex)
    {
        Gun pickedGun = allGuns[allGunIndex];

        int replaceIndex = playerController.currentGunIndex;

        // �رվ�ǹ
        playerController.guns[replaceIndex].gameObject.SetActive(false);

        // �滻
        Gun newGun = Instantiate(pickedGun); // ��ʵ����
        newGun.transform.SetParent(playerController.gunHolder1); // �ؼ����趨������Ϊ gunHolder
        newGun.transform.localPosition = Vector3.zero;
        newGun.transform.localRotation = Quaternion.identity;

        // 3. ��������
        playerController.guns[replaceIndex] = newGun;
        playerController.activeGun = newGun;

        Debug.Log("�Ѹ���Ϊ��������" + newGun.name);
    }
}