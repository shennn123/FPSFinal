using UnityEngine;

//串联AI、生命值、攻击、状态同步模块
public class MonsterController : MonoBehaviour
{
    //用于在 MonsterManager 中唯一标识每只怪物，便于全局访问、修改和管理。
    public int monsterId;

    //引用模块
    private MonsterAI ai;
    private MonsterHealth health;
    private MonsterAttack attack;
    private Animator animator;

    //初始化
    void Start()
    {
        //自动生成ID
        monsterId = IDGenerator.GetNextId();

        //减少每一帧调用 GetComponent() 的性能开销，并确保这些模块都可被统一调用。
        ai = GetComponent<MonsterAI>();
        health = GetComponent<MonsterHealth>();
        attack = GetComponent<MonsterAttack>();
        animator = GetComponent<Animator>();

        //将怪物数据注册到 MonsterManager（单例），便于统一管理多个怪物。
        MonsterData data = new MonsterData(gameObject, health.maxHealth, "Idle");
        MonsterManager.Instance.RegisterMonster(monsterId, data);

        // 初始状态
        SetState("Idle");
    }

    //每帧执行
    void Update()
    {
        //调用ai模块，处理追踪、攻击、闲置等行为
        ai.ProcessAI();
    }

    //允许这个怪物将自己的状态（如 "Attacking"、"Dead"）同步到 MonsterManager。
    public void SetState(string newState)
    {
        MonsterManager.Instance.UpdateMonsterState(monsterId, newState);

        Debug.Log("Set state: " + newState);

        // 播放对应动画
        if (animator != null)
        {
            // 清除所有触发器，防止状态冲突
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Chasing");
            animator.ResetTrigger("Attacking");
            animator.ResetTrigger("Dead");

            // 设置当前状态触发器
            animator.SetTrigger(newState);
            
        }
    }

    public void OnDeath()
    {
        //怪物死亡时，必须释放它占用的资源（物体 + 状态数据）。
        MonsterManager.Instance.RemoveMonster(monsterId);
        Destroy(gameObject,2f);// 数字表示 延迟销毁给死亡动画时间播放
    }
}
