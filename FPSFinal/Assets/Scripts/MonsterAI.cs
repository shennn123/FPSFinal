using UnityEngine;
//引入 导航系统 的命名空间，支持使用 NavMeshAgent 进行 AI 路径寻路
using UnityEngine.AI;

//控制怪物的行为
public class MonsterAI : MonoBehaviour
{
    //定义行为的范围
    public float chaseRange = 10f;
    public float attackRange = 3f;

    //记录玩家的位置（用于导航和攻击）
    private Transform player;

    //怪物身上的 NavMeshAgent，负责自动寻路
    private NavMeshAgent agent;

    //引用 MonsterController，用来设置状态
    private MonsterController controller;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<MonsterController>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    //根据距离执行活动及动画
    public void ProcessAI()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < attackRange)
        {
            agent.ResetPath();
            controller.SetState("Attacking");
        }
        else if (dist < chaseRange)
        {
            agent.SetDestination(player.position);
            controller.SetState("Chasing");
        }
        else
        {
            agent.ResetPath();
            controller.SetState("Idle");
        }
    }
}

