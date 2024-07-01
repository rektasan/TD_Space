using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    //New 29.06 Currency
    [SerializeField] private int currencyWorth = 50;
    //

    private bool isDestroyed = false; //Fix 22.06

    public void TakeDamage(int dmg)
    {
        // hp -= 1; -> hp = hp - 1;
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed) //Fix 22.06 (! - not)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            //New 29.06 Currency
            LevelManager.main.IncreaseCurrency(currencyWorth);
            //
            isDestroyed = true; //Fix 22.06
            Destroy(gameObject);
        }
    }
}
