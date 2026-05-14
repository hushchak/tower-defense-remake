using System;
using UnityEngine;

public class ChickenSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ChickenTower chicken;
    [Space]
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;

    private void OnEnable()
    {
        chicken.OnHalfCooked += HandleHalfCooked;
        chicken.OnFullyCooked += HandleFullyCooked;
    }

    private void OnDisable()
    {
        chicken.OnHalfCooked += HandleHalfCooked;
        chicken.OnFullyCooked += HandleFullyCooked;
    }

    private void HandleHalfCooked() => spriteRenderer.sprite = sprite1;
    private void HandleFullyCooked() => spriteRenderer.sprite = sprite2;
}
