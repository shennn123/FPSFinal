using UnityEngine;

public class PreSet : MonoBehaviour
{
    void Start()
    {
        int[] damageList = new int[] { 20, 10, 90, 10, 30 };

        Gun[] guns = PlayerController.instance.guns;

        for (int i = 0; i < guns.Length && i < damageList.Length; i++)
        {
            Gun gun = guns[i];
            if (gun != null && gun.bulletPrefab != null)
            {
                BulletController bullet = gun.bulletPrefab.GetComponent<BulletController>();
                if (bullet != null)
                {
                    bullet.Damage = damageList[i];
                    Debug.Log($"����ǹ{i}��{gun.name}�����ӵ��˺�Ϊ {damageList[i]}");
                }
                else
                {
                    Debug.LogWarning($"ǹ{i}��{gun.name}�����ӵ���û�� BulletController �����");
                }
            }
            else
            {
                Debug.LogWarning($"ǹ{i} Ϊ�ջ�δ���ӵ� prefab��");
            }
        }
    }
}
