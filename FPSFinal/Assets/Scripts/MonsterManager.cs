//该命名空间允许使用泛型容器来存储大量怪物的数据
using System.Collections.Generic;
using UnityEngine;

//对怪物的状态进行全局管理
public class MonsterManager : MonoBehaviour
{
    //定义一个 静态变量 用来实现 单例模式，记得挂在空物体上
    public static MonsterManager Instance;

    //定义一个 Dictionary（字典）来存储所有怪物数据
    private Dictionary<int, MonsterData> monsterDict = new Dictionary<int, MonsterData>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
       UIManager.Show("InGameUI");
    }

    //该函数用来注册怪物
    public void RegisterMonster(int id, MonsterData data)
    {
        if (!monsterDict.ContainsKey(id))
            monsterDict.Add(id, data);
    }

    //该函数用来更新怪物血量
    public void UpdateMonsterHealth(int id, int newHealth)
    {
        if (monsterDict.ContainsKey(id))
            monsterDict[id].health = newHealth;
    }

    //该函数用来更新怪物状态
    public void UpdateMonsterState(int id, string newState)
    {
        if (monsterDict.ContainsKey(id))
            monsterDict[id].status = newState;
    }

    //更新怪物当前位置
    public void UpdateMonsterPosition(int id, Vector3 newPos)
    {
        if (monsterDict.ContainsKey(id))
            monsterDict[id].position = newPos;
    }

    //获取怪物数据
    public MonsterData GetMonsterData(int id)
    {
        return monsterDict.ContainsKey(id) ? monsterDict[id] : null;
    }

    //移除怪物数据
    public void RemoveMonster(int id)
    {
        if (monsterDict.ContainsKey(id))
            monsterDict.Remove(id);
    }
}
