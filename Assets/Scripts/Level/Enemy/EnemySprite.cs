using System;
using UnityEngine;

public class EnemySprite : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private GameObject spriteObject;

    private void OnEnable()
    {
        enemyMovement.OnDirectionChanged += HandleDirectionChange;
    }

    private void OnDisable()
    {
        enemyMovement.OnDirectionChanged -= HandleDirectionChange;
    }

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
