using UnityEngine;

public class WeaponPickupTrigger : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public int gunIndexInAllGuns; // �������ǹ�� allGuns �����е�����

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {

            PlayerController pc = PlayerController.instance;

            // ����һ������ҵ��ƶ����������ж�
            if (pc.currentSpeed > 0.1f)
            {
                Debug.Log("�������ƶ�ʱ��ǹ��");
                return;
            }

            // ��ȡ��ǰ���ϵ�ǹ�͵��ϵ�ǹ
            GameObject currentGun = pc.guns[pc.currentGunIndex].gameObject;
            GameObject groundGun = pc.guns[gunIndexInAllGuns].gameObject;

            // ���õ���λ����΢�ϸ�
            Vector3 dropPos = transform.parent.position+ Vector3.up * 1.3f;

            GameObject droppedGun = Instantiate(pc.gunPrefabs[pc.currentGunIndex], dropPos, Quaternion.identity);

            droppedGun.transform.SetParent(null); // ȷ�����ǽ�ɫ������

            // �������ǹ 
            droppedGun.SetActive(true);



            //��� Trigger �ͽ����߼�
            BoxCollider col = droppedGun.AddComponent<BoxCollider>();
            col.isTrigger = true;

            WeaponPickupTrigger trigger = droppedGun.AddComponent<WeaponPickupTrigger>();
            trigger.gunIndexInAllGuns = gunIndexInAllGuns;

            //�滻 guns �����е����� 
            pc.guns[pc.currentGunIndex] = pc.guns[gunIndexInAllGuns];

            // �л� activeGun 
            pc.activeGun.gameObject.SetActive(false);
            pc.activeGun = pc.guns[pc.currentGunIndex];
            pc.activeGun.gameObject.SetActive(true);

            //�Ƴ�ԭ trigger ���󣨵��Ͼ�ǹ��
            Destroy(transform.parent.gameObject);
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
