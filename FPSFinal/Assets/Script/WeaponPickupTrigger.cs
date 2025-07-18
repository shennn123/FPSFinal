using UnityEngine;

public class WeaponPickupTrigger : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public int gunIndexInAllGuns; // 0 ~ 3����ʾ����һ��ǹ����Ӧ allGuns ���飩

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            PlayerController pc = PlayerController.instance;

            // ���ö�Ӧ����Ϊ����״̬
            switch (gunIndexInAllGuns)
            {
                case 0:
                    pc.gun1Unlocked = true;
                    break;
                case 1:
                    pc.gun2Unlocked = true;
                    break;
                case 2:
                    pc.gun3Unlocked = true;
                    break;
                case 3:
                    pc.gun4Unlocked = true;
                    break;
                default:
                    Debug.LogWarning("δ֪ǹ������" + gunIndexInAllGuns);
                    break;
            }

            Debug.Log($"��ʰȡ���� {gunIndexInAllGuns + 1}���ѽ�����");

            // ɾ���������ǹ
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