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

    //血条的UI
    public  UnityEngine.UI.Slider MonsterHPUI;

    //相机用于将血条面向玩家
    private Camera mainCamera;

    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<MonsterController>();

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
        currentHealth -= damage;
        controller.animator.SetTrigger("GetHit");
        //Debug.Log("怪物当前血量为：" + currentHealth);

        //同步这个怪物的最新血量到全局的 MonsterManager
        MonsterManager.Instance.UpdateMonsterHealth(controller.monsterId, currentHealth);

        //受伤时立刻更新血条
        UpdateMonsterHealthBar(currentHealth, maxHealth);

        //生命值为0触发死亡
        if (currentHealth <= 0)
        {
            controller.OnDeath();
        }
    }

    //血条值的计算
    public void UpdateMonsterHealthBar(int current, int max)
    {
        if (MonsterHPUI != null)
        {
            MonsterHPUI.value = (float)current / max;
        }
        
    }
}
