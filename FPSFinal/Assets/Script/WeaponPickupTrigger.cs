using UnityEngine;

public class WeaponPickupTrigger : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public int gunIndexInAllGuns; // 0 ~ 3，表示是哪一把枪（对应 allGuns 数组）

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            PlayerController pc = PlayerController.instance;

            // 设置对应武器为解锁状态
            switch (gunIndexInAllGuns)
            {
                case 0:
                    pc.gun1Unlocked = true;
                    PlayerController.instance.SwitchGun(0); // 自动切换到第一把枪
                    break;
                case 1:
                    pc.gun2Unlocked = true;
                    PlayerController.instance.SwitchGun(1); // 自动切换到第一把枪
                    break;
                case 2:
                    pc.gun3Unlocked = true;
                    PlayerController.instance.SwitchGun(2); // 自动切换到第一把枪
                    break;
                case 3:
                    pc.gun4Unlocked = true;
                    PlayerController.instance.SwitchGun(3); // 自动切换到第一把枪
                    break;
                default:
                    Debug.LogWarning("未知枪索引：" + gunIndexInAllGuns);
                    break;
            }

            Debug.Log($"已拾取武器 {gunIndexInAllGuns + 1}，已解锁！");

            // 删除地上这把枪
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            UIPickupPrompt.instance?.Show();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            UIPickupPrompt.instance?.Hide();
        }
    }
}