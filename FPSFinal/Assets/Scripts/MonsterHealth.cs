using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Collections;

//管理怪物的生命值逻辑，比如受伤、死亡
public class MonsterHealth : MonoBehaviour
{
    //设置怪物默认血量上限
    public int maxHealth = 100;

    //用于动态追踪怪物在游戏过程中的生命状态
    private int currentHealth;

    //引用 controller，便于在怪物死亡或受伤时，更新怪物的全局状态或执行销毁逻辑
    private MonsterController controller;

    //血条的UI
    public UnityEngine.UI.Slider MonsterHPUI;

    //相机用于将血条面向玩家
    private Camera mainCamera;

    //用于死亡时屏蔽受击动画
    public bool isMonsterDead = false;

    //做受击冷却
    private float hitCooldown = 1.5f; // 设置冷却时间（秒）
    private float lastHitTime = -999f; // 上次受击时间
    private NavMeshAgent agent; // 引用 NavMeshAgent


    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<MonsterController>();
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;

        //初始化血条的数值和相机
        UpdateMonsterHealthBar(currentHealth, maxHealth);

        //如果有prefab可以用这个来获取血条
        //GetComponentInChildren<Slider>()
    }

    public void Update()
    {
        //血条始终面向主相机
        MonsterHPUI.transform.rotation = Quaternion.LookRotation(MonsterHPUI.transform.position - mainCamera.transform.position);
    }

    //让其他对象可以通过调用这个方法来“打”怪物。
    public void TakeDamage(int damage)
    {
        //怪物如果死亡直接返回
        if (isMonsterDead) return;

        currentHealth -= damage;

        //生命值为0触发死亡
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isMonsterDead = true;
            controller.OnDeath();
        }
        else
        {
            // 加入受击动画冷却限制
            if (Time.time - lastHitTime > hitCooldown)
            {
                controller.animator.SetTrigger("GetHit");
                StartCoroutine(FreezeMovementDuringHit());//僵直
                lastHitTime = Time.time;
            }
        }

        //同步这个怪物的最新血量到全局的 MonsterManager
        MonsterManager.Instance.UpdateMonsterHealth(controller.monsterId, currentHealth);

        //受伤时立刻更新血条
        UpdateMonsterHealthBar(currentHealth, maxHealth);
    }

    //血条值的计算
    public void UpdateMonsterHealthBar(int current, int max)
    {
        if (MonsterHPUI != null)
        {
            MonsterHPUI.value = (float)current / max;
        }

    }

    //携程僵直
    private IEnumerator FreezeMovementDuringHit()
    {
        if (agent != null)
        {
            agent.isStopped = true; // 暂停 AI 移动
        }

        // 等待动画播放完（你可以根据动画时长来调整）
        yield return new WaitForSeconds(0.8f); // 受击动画时长

        if (agent != null && !isMonsterDead) // 确保怪还活着
        {
            agent.isStopped = false; // 恢复移动
        }
    }
}
