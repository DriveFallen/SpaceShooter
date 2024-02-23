using UnityEngine;

public class TrapBomb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isEnemy = collision.gameObject.TryGetComponent(out Enemy enemy);

        if (isEnemy)
        {
            enemy.Kill();
            Destroy(gameObject);
        }
    }
}
