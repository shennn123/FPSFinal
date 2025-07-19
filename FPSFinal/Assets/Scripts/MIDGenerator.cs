using UnityEngine;

//在运行时自动生成给每个怪物生成id
public static class IDGenerator
{
    private static int currentId = 0;

    public static int GetNextId()
    {
        return currentId++;
    }
}
