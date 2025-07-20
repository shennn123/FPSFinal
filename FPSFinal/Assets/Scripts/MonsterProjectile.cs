using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    //投射物伤害、速度、生命周期
    public int damage = 10;
    public float speed = 10f;
    public float lifeTime = 10f;

    private Vector3 moveDirection;  // 锁定方向

    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir.normalized;

        // 让“头朝上”的模型朝向目标：将飞行方向当作 Y+ 的方向
        Quaternion lookRot = Quaternion.LookRotation(moveDirection);      // 默认是让 Z+ 对准方向
        Quaternion adjustment = Quaternion.Euler(90f, 0f, 0f);           // 把 Y+ 转向 Z+
        transform.rotation = lookRot * adjustment;
    }


    private void Start()
    {
        Destroy(gameObject, lifeTime); // 读秒后自动销毁
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;//移动
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          other.gameObject.GetComponent<PlayerHealthController>()?.DamagePlayer(damage); // 调用玩家的受伤方法
            Debug.Log("远程攻击命中了玩家，造成了伤害：" + damage);
            Destroy(this.gameObject); // 命中后销毁
        }
        else if (!other.CompareTag("Monster")) // 避免命中自己
        {
            Destroy(gameObject); // 撞到其他物体也销毁
        }
    }
}
