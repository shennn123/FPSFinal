using UnityEngine;

//不继承自 MonoBehaviour，纯数据类，不可以挂在场景中的 GameObject 上
public class MonsterData
{
    //以下为单个怪物的数据的定义
    public GameObject monsterObject;
    public int health;
    public Vector3 position;
    public string status; // Idle, Chasing, Attacking, Dead

    //该函数用于赋予单个怪物的数据
    public MonsterData(GameObject obj, int hp, string state)
    {
        monsterObject = obj;
        health = hp;
        position = obj.transform.position;
        status = state;
    }

    //该函数用于更新单个怪物当前位置
    public void UpdatePosition()
    {
        if (monsterObject != null)
            position = monsterObject.transform.position;
    }
}
