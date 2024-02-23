using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float BulletSpeed = 15f;
    public int MinBulletDamage = 10;
    public int MaxBulletDamage = 20;

    private int _bulletDamage;
    private Rigidbody2D _rigidbody;
    private Transform _transform;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        _rigidbody.velocity = _transform.up * BulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        bool isEnemy = collision.gameObject.TryGetComponent(out Enemy enemy);

        if (isEnemy)
        {
            _bulletDamage = Random.Range(MinBulletDamage, MaxBulletDamage);
            enemy.TakeDamage(_bulletDamage);
            Destroy(gameObject);
        }
    }
}