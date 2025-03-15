using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    private Dictionary<string, bool> enemyStates = new Dictionary<string, bool>();

    public void SaveEnemyState(string enemyID, bool isDead)
    {
        enemyStates[enemyID] = isDead;
    }

    public bool LoadEnemyState(string enemyID)
    {
        bool isDead = false;
        if (enemyStates.TryGetValue(enemyID, out isDead))
        {
            return isDead;
        }
        return false; // Default to alive if not found
    }
}
