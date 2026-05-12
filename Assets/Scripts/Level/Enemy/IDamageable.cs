public interface IDamageable
{
    public void ApplyDamage(int damage, DamageType type, SlownessData slownessData);
    public bool CanBeDamaged(DamageType type);
}
