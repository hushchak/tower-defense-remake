using System;
using UnityEngine;

public class EnemySprite : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyEffects enemyEffects;
    [SerializeField] private GameObject spriteObject;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Space]
    [SerializeField] private Color regularColor = Color.white;
    [SerializeField] private Color tarredColor = new Color(133, 95, 78);

    private void OnEnable()
    {
        enemyMovement.OnDirectionChanged += HandleDirectionChange;
        enemyEffects.OnTarred += HandleTarred;

        spriteRenderer.color = regularColor;
    }

    private void OnDisable()
    {
        enemyMovement.OnDirectionChanged -= HandleDirectionChange;
        enemyEffects.OnTarred -= HandleTarred;
    }

    private void HandleTarred(bool tarred) => spriteRenderer.color = tarred ? tarredColor : regularColor;

    private void HandleDirectionChange(Vector2 vector)
    {
        if (Mathf.Abs(vector.x) < 0.1f)
            return;

        if (vector.x > 0.4f)
        {
            spriteObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (vector.x < 0.4f)
        {
            spriteObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
