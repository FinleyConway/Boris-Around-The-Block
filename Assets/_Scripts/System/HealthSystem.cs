using System;

public class HealthSystem
{
    public event EventHandler OnHealthChange;
    public event EventHandler OnDead;

    private int health;
    private int maxHealth;

    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0;
            Death();
        }
        OnHealthChange?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        OnHealthChange?.Invoke(this, EventArgs.Empty);
    }

    public void Death()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }
}
