using System;
using UnityEngine;

public static class GameManager
{
    public static int killsNeeded = 10;

    private static int killCount = 0;
    private static bool firstDoorUnlocked = false;
    private static bool bossKilled = false;

    public static event Action OnFirstDoorUnlocked;
    public static event Action OnBossKilled;

    public static void EnemyKilled(bool isCommon)
    {
        if (isCommon && !firstDoorUnlocked)
        {
            killCount++;
            if (killCount >= killsNeeded)
            {
                firstDoorUnlocked = true;
                OnFirstDoorUnlocked?.Invoke();
            }
        }
    }

    public static void BossKilled()
    {
        if (!bossKilled)
        {
            bossKilled = true;
            OnBossKilled?.Invoke();
        }
    }
}
