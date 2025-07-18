using UnityEngine;

public class WeaponPickupTrigger : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public int gunIndexInAllGuns; // 地上这把枪在 allGuns 数组中的索引
    public bool isPrimaryWeapon = false; // 是否是主武器，如果是则永远放在 guns[0]


    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {

            PlayerController pc = PlayerController.instance;

            if (pc.currentGunIndex == 0 && !isPrimaryWeapon)
            {
                Debug.Log("当前手上是步枪，不能捡手枪！");
                return;
            }
            else if (pc.currentGunIndex == 1 && isPrimaryWeapon)
            {
                Debug.Log("当前手上是手枪，不能捡步枪！");
                return;
            }

            // 获取当前手上的枪和地上的枪
            GameObject currentGun = pc.guns[pc.currentGunIndex].gameObject;
            GameObject groundGun = pc.gunPrefabs[gunIndexInAllGuns].gameObject;

            // 设置掉落位置稍微上浮
            // 设置掉落位置稍微上浮（使用最上层 parent 的位置）
            Transform topParent = transform;
            while (topParent.parent != null)
            {
                topParent = topParent.parent;
            }

            Vector3 dropPos = topParent.position + Vector3.up * 1.3f;
            Quaternion dropRot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);



            GameObject droppedGun = Instantiate(pc.gunPrefabs[pc.currentGunIndex], dropPos, dropRot);

            droppedGun.transform.SetParent(null); // 确保不是角色子物体

            // 激活掉落枪 
            droppedGun.SetActive(true);



            //添加 Trigger 和交互逻辑
            BoxCollider col = droppedGun.AddComponent<BoxCollider>();
            col.isTrigger = true;

            WeaponPickupTrigger trigger = droppedGun.AddComponent<WeaponPickupTrigger>();
            trigger.gunIndexInAllGuns = gunIndexInAllGuns;

            //替换 guns 数组中的引用 
            int targetIndex = isPrimaryWeapon ? 0 : 1;

            // 替换目标槽的枪
            pc.guns[targetIndex] = pc.guns[gunIndexInAllGuns];

            // 更新当前武器索引
            pc.currentGunIndex = targetIndex;

            // 切换 activeGun 
            pc.activeGun.gameObject.SetActive(false);
            pc.activeGun = pc.guns[pc.currentGunIndex];
            pc.activeGun.gameObject.SetActive(true);

            //移除原 trigger 对象（地上旧枪）
            while (topParent.parent != null)
            {
                topParent = topParent.parent;
            }
            Destroy(topParent.gameObject);
        }

    }

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
}