using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HealthSystem
{
    public static void TakeDamage(int amount, Entity target = null)
    {
        if (!target)
            target = EntityManager.TargetCharacter;

        var currentHealth = target.HP;
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            OnDeath(target);
            target.Hit = true;
            if (currentHealth < 0)
                currentHealth = OnOverkill();
        }

        target.HP = currentHealth;
        target.ChangeHealth();
    }
    public static void Heal(Entity target, int amount)
    {
        var currentHealth = target.HP;
        for (int i = amount; i > 0; i--)
        {
            if (i + currentHealth <= target.MaxHP)
            {
                currentHealth += i;
                target.HP = currentHealth;
                target.ChangeHealth();
                break;
            }
        }
    }
    private static void OnDeath(Entity toKill)
    {
        toKill.Dead = true;
    }
    private static int OnOverkill()
    {
        return 0;
    }
    private static void OnHit(int damage)
    {
        var target = EntityManager.TargetCharacter;
        var executor = EntityManager.ExecutorCharacter;
    }
}
