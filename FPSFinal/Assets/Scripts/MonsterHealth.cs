using UnityEngine;

//管理怪物的生命值逻辑，比如受伤、死亡
public class MonsterHealth : MonoBehaviour
{
    //设置怪物默认血量上限
    public int maxHealth = 100;

    //用于动态追踪怪物在游戏过程中的生命状态
    private int currentHealth;

    //引用 controller，便于在怪物死亡或受伤时，更新怪物的全局状态或执行销毁逻辑
    private MonsterController controller;

    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<MonsterController>();
    }

    //让其他对象可以通过调用这个方法来“打”怪物。
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        //同步这个怪物的最新血量到全局的 MonsterManager
        MonsterManager.Instance.UpdateMonsterHealth(controller.monsterId, currentHealth);

        if (currentHealth <= 0)
        {
            controller.SetState("Dead");
            controller.OnDeath();
        }
    }
}
