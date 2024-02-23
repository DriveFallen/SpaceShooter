using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField] private int healAmount = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isPlayer = collision.gameObject.TryGetComponent(out Player player);

        if (isPlayer)
        {
            player.ApplyHeal(healAmount);
            Destroy(gameObject);
        }
    }
}
