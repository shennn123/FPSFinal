using UnityEngine;

public static class IDGenerator
{
    private static int currentId = 0;

    public static int GetNextId()
    {
        return currentId++;
    }
}
