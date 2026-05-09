using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private ProjectileData data;
    private Vector2 targetPosition;

    public void Setup(ProjectileData data)
    {
        this.data = data;
        targetPosition = (Vector2)transform.position + data.Direction * data.Distance;

        spriteRenderer.sprite = data.Sprite;
    }

    private void Update()
    {
        //if (SessionStateManager.Instance.IsPaused)
        //    return;

        MoveToTarget(Time.deltaTime);
        CheckEnemies();
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            Dissolve();
        }
    }

    private void MoveToTarget(float delta)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, data.Speed * delta);
    }


    private void CheckEnemies()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, data.Radius, data.EnemyLayer);

        if (enemy != null)
        {
            if (enemy.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(data.Damage);
                TargetReached();
            }
        }
    }

    private void TargetReached() => gameObject.SetActive(false);
    private void Dissolve() => gameObject.SetActive(false);
}
