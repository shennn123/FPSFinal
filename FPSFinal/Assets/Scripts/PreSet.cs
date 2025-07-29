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
                    Debug.Log($"设置枪{i}（{gun.name}）的子弹伤害为 {damageList[i]}");
                }
                else
                {
                    Debug.LogWarning($"枪{i}（{gun.name}）的子弹上没有 BulletController 组件！");
                }
            }
            else
            {
                Debug.LogWarning($"枪{i} 为空或未绑定子弹 prefab！");
            }
        }
    }
}
