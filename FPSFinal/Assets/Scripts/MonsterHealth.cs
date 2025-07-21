using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//管理怪物的生命值逻辑，比如受伤、死亡
public class MonsterHealth : MonoBehaviour
{
    //设置怪物默认血量上限
    public int maxHealth = 100;

    //用于动态追踪怪物在游戏过程中的生命状态
    private int currentHealth;

    //引用 controller，便于在怪物死亡或受伤时，更新怪物的全局状态或执行销毁逻辑
    private MonsterController controller;
    public  UnityEngine.UI.Slider MonsterHPUI;
    private Camera mainCamera;

    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<MonsterController>();
        UpdateMonsterHealthBar(currentHealth, maxHealth);
        mainCamera = Camera.main;

    }

    //让其他对象可以通过调用这个方法来“打”怪物。
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        controller.animator.SetTrigger("GetHit");
        
        //同步这个怪物的最新血量到全局的 MonsterManager
        MonsterManager.Instance.UpdateMonsterHealth(controller.monsterId, currentHealth);
        UpdateMonsterHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            controller.OnDeath();
        }
    }
    public void Update()
    {
            // 始终面向主相机
            MonsterHPUI.transform.rotation = Quaternion.LookRotation(MonsterHPUI.transform.position - mainCamera.transform.position);
    }
    public void UpdateMonsterHealthBar(int current, int max)
    {
        if (MonsterHPUI != null)
        {
            MonsterHPUI.value = (float)current / max;
        }
    }
}
