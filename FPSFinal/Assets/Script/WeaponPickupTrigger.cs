using UnityEngine;

public class WeaponPickupTrigger : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public int gunIndexInAllGuns; // 地上这把枪在 allGuns 数组中的索引

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {

            PlayerController pc = PlayerController.instance;

            if (pc.currentGunIndex == 0 && gunIndexInAllGuns != 0)
            {
                Debug.Log("当前手上是步枪，不能捡手枪！");
                return;
            }
            else if (pc.currentGunIndex == 1 && gunIndexInAllGuns != 1)
            {
                Debug.Log("当前手上是手枪，不能捡步枪！");
                return;
            }
            // 获取当前手上的枪和地上的枪
            GameObject currentGun = pc.guns[pc.currentGunIndex].gameObject;
            GameObject groundGun = pc.guns[gunIndexInAllGuns].gameObject;

            // 设置掉落位置稍微上浮
            Vector3 dropPos = transform.parent.position + Vector3.up * 1.3f;

            GameObject droppedGun = Instantiate(pc.gunPrefabs[pc.currentGunIndex], dropPos, Quaternion.identity);

            droppedGun.transform.SetParent(null); // 确保不是角色子物体

            // 激活掉落枪 
            droppedGun.SetActive(true);



            //添加 Trigger 和交互逻辑
            BoxCollider col = droppedGun.AddComponent<BoxCollider>();
            col.isTrigger = true;

            WeaponPickupTrigger trigger = droppedGun.AddComponent<WeaponPickupTrigger>();
            trigger.gunIndexInAllGuns = gunIndexInAllGuns;

            //替换 guns 数组中的引用 
            pc.guns[pc.currentGunIndex] = pc.guns[gunIndexInAllGuns];

            // 切换 activeGun 
            pc.activeGun.gameObject.SetActive(false);
            pc.activeGun = pc.guns[pc.currentGunIndex];
            pc.activeGun.gameObject.SetActive(true);

            //移除原 trigger 对象（地上旧枪）
            Transform topParent = transform;
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