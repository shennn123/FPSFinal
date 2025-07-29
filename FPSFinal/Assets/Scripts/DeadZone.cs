using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered DeadZone!");

            // ��������ʽ1��ֱ�ӵ��� GameManager ����������
            other.gameObject.SetActive(false); // ������Ҷ���
            GameManager.instance.PlayerDied();

            // ��ʽ2������ PlayerHealthController �������߼�
            // PlayerHealthController.instance.DamagePlayer(9999, true);
        }
    }
}
