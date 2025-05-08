using UnityEngine;

public class DefensiveWall : Damagable
{
    [SerializeField] HealthBarController healthBarController;
    int index;
    TownHall townHall;

    public void SetTownHall(TownHall townHall, int index)
    {
        this.townHall = townHall;
        this.index = index;
        SetTeam(townHall.GetTeam());
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBarController.UpdateHealth(GetHealth(), GetMaxHealth());
    }

    public override void OnDeath()
    {
        base.OnDeath();
        townHall.ClearWall(index);
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        healthBarController.UpdateHealth(GetHealth(), GetMaxHealth());
    }
}
