using UnityEngine;

//用于协程函数
using System.Collections;


//串联AI、生命值、攻击、状态同步模块
public class MonsterController : MonoBehaviour
{
    //用于在 MonsterManager 中唯一标识每只怪物，便于全局访问、修改和管理。
    public int monsterId;

    // 用来记录当前动画状态以解决重复触发trigger
    private string currentState = "";

    //引用模块
    private MonsterAI ai;
    private MonsterHealth health;
    private MonsterAttack attack;
    public Animator animator;

    //初始化
    void Start()
    {
        //自动生成ID
        monsterId = IDGenerator.GetNextId();

        //减少每一帧调用 GetComponent() 的性能开销，并确保这些模块都可被统一调用。
        ai = GetComponent<MonsterAI>();
        health = GetComponent<MonsterHealth>();
        attack = GetComponent<MonsterAttack>();

        //动画机在子物体上
        animator = GetComponentInChildren<Animator>();

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

        // 播放对应动画
        if (animator != null)
        {
            if (newState != currentState && animator != null)
            {
                // 清除旧的触发器，防止冲突
                animator.ResetTrigger("Idle");
                animator.ResetTrigger("Chasing");
                animator.ResetTrigger("Attacking");

                // 触发新的动画状态
                animator.SetTrigger(newState);

                // 更新当前状态
                currentState = newState;

            }
        }
    }

    public void OnDeath()
    {
        Debug.Log("Monster " + monsterId + " has died.");

        //动画播放死亡动画
        animator.ResetTrigger("GetHit"); // 清除受击触发器,确保死亡触发时不会是受击中动作
        animator.SetTrigger("Dead");

        //销毁数据
        MonsterManager.Instance.RemoveMonster(monsterId);

        // 启动延迟销毁协程（自动获取死亡动画长度）
        StartCoroutine(DestroyAfterDeathAnimation());
    }


    //销毁协程
    private IEnumerator DestroyAfterDeathAnimation()
    {
        // 等待 1 帧，确保 Animator 切换到 Dead 状态
        yield return null;

        // 获取当前动画状态
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 等待当前动画长度播放完成（可以加减毫秒来微调，默认为0）
        float waitTime = stateInfo.length - 0.0f;
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }
}


