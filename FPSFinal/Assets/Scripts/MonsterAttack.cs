using UnityEngine;

//两种攻击类型：近战和远程
public enum AttackType
{
    Melee,
    Ranged
}

public class MonsterAttack : MonoBehaviour
{
    // 在 Inspector 中选择攻击类型
    public AttackType attackType = AttackType.Melee;

    //近战攻击的伤害
    public int damage = 10;

    [Header("Ranged Attack Settings")]
    // 投射物预制体
    public GameObject projectilePrefab;
    public Transform firePoint;// 发射位置

    private Animator animator;

    //检测本轮攻击是否已经命中
    private bool hasDealtDamage = false;


    private float lastAttackTime = -999f;
    private float attackCooldown = 0.5f; // 半秒内只允许一次攻击

    //引用怪物血量模组
    private MonsterHealth mh;

    //攻击动画开始的时刻调用
    void Start()
    {
        //因为脚本需要挂在攻击部位上，所以动画机和其他模组都得往上找--败笔，但懒得改了
        animator = GetComponentInParent<Animator>();
        mh = GetComponentInParent<MonsterHealth>();
    }

    void Update()
    {
        if (mh.isMonsterDead) return; // 死亡后停止攻击

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //如果当前动画状态是攻击
        if (stateInfo.IsName("Attacking"))
        {
            float animTime = Mathf.Repeat(stateInfo.normalizedTime, 1f);

            if (animTime < 0.05f && Time.time - lastAttackTime > attackCooldown)
            {
                lastAttackTime = Time.time;
                PerformRangedAttack();
            }
        }
    }

    //发射投射物
    private void PerformRangedAttack()
    {
        //如果是远程攻击就执行
        if (attackType != AttackType.Ranged) return;

        if (projectilePrefab != null && firePoint != null)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null) return;

            Vector3 direction = (player.position - firePoint.position).normalized;

            //生成投射物
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            //投射物的伤害
            MonsterProjectile p = projectile.GetComponent<MonsterProjectile>();
            p.damage = this.damage;
            p.SetDirection(direction);  // 发射前锁定方向
        }
    }

    //绑定该代码的物体触碰到对应tag时执行
    private void OnTriggerEnter(Collider other)
    {
        //如果是近战攻击就执行
        if (attackType != AttackType.Melee) return;

        //如果已经攻击了就不再触发
        //if (hasDealtDamage) return;

        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealthController>()?.DamagePlayer(damage, true); // Call TakeDamage on the MonsterController if it exists
        }
    }
}

