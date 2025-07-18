using UnityEngine;

public class WeaponPickupTrigger : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public int gunIndexInAllGuns; // �������ǹ�� allGuns �����е�����
    public bool isPrimaryWeapon = false; // �Ƿ��������������������Զ���� guns[0]


    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {

            PlayerController pc = PlayerController.instance;

            if (pc.currentGunIndex == 0 && !isPrimaryWeapon)
            {
                Debug.Log("��ǰ�����ǲ�ǹ�����ܼ���ǹ��");
                return;
            }
            else if (pc.currentGunIndex == 1 && isPrimaryWeapon)
            {
                Debug.Log("��ǰ��������ǹ�����ܼ�ǹ��");
                return;
            }

            // ��ȡ��ǰ���ϵ�ǹ�͵��ϵ�ǹ
            GameObject currentGun = pc.guns[pc.currentGunIndex].gameObject;
            GameObject groundGun = pc.gunPrefabs[gunIndexInAllGuns].gameObject;

            // ���õ���λ����΢�ϸ�
            // ���õ���λ����΢�ϸ���ʹ�����ϲ� parent ��λ�ã�
            Transform topParent = transform;
            while (topParent.parent != null)
            {
                topParent = topParent.parent;
            }

            Vector3 dropPos = topParent.position + Vector3.up * 1.3f;
            Quaternion dropRot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);



            GameObject droppedGun = Instantiate(pc.gunPrefabs[pc.currentGunIndex], dropPos, dropRot);

            droppedGun.transform.SetParent(null); // ȷ�����ǽ�ɫ������

            // �������ǹ 
            droppedGun.SetActive(true);



            //��� Trigger �ͽ����߼�
            BoxCollider col = droppedGun.AddComponent<BoxCollider>();
            col.isTrigger = true;

            WeaponPickupTrigger trigger = droppedGun.AddComponent<WeaponPickupTrigger>();
            trigger.gunIndexInAllGuns = gunIndexInAllGuns;

            //�滻 guns �����е����� 
            int targetIndex = isPrimaryWeapon ? 0 : 1;

            // �滻Ŀ��۵�ǹ
            pc.guns[targetIndex] = pc.guns[gunIndexInAllGuns];

            // ���µ�ǰ��������
            pc.currentGunIndex = targetIndex;

            // �л� activeGun 
            pc.activeGun.gameObject.SetActive(false);
            pc.activeGun = pc.guns[pc.currentGunIndex];
            pc.activeGun.gameObject.SetActive(true);

            //�Ƴ�ԭ trigger ���󣨵��Ͼ�ǹ��
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