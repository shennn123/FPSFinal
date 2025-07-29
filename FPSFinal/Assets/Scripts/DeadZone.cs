using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered DeadZone!");

            // 死亡处理方式1：直接调用 GameManager 的死亡方法
            other.gameObject.SetActive(false); // 禁用玩家对象
            GameManager.instance.PlayerDied();

            // 或方式2：调用 PlayerHealthController 的死亡逻辑
            // PlayerHealthController.instance.DamagePlayer(9999, true);
        }
    }
}
