using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Camera _camera;
    private Vector2 _mousePosition;
    private Vector2 _lookDirection;

    private int _health = 100;
    public int Health
    {
        get 
        { 
            return _health; 
        }
        set 
        { 
            _health = value; 
            GameEvents.Instance.InvokePlayerHealthChanged(Health); 
        }
    }
    public static bool ShouldRotate = true;
    [SerializeField] private ParticleSystem deathParticles;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (!ShouldRotate) return;
        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _lookDirection = _mousePosition - _rb.position;
        float angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var isEnemy = collision.gameObject.TryGetComponent(out Enemy enemy);
        if (isEnemy)
        {
            enemy.Kill();
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        if (_health <= 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        GameEvents.Instance.InvokePlayerKilledEvent();
        gameObject.SetActive(false);

        if (deathParticles == null) { Debug.LogWarning("Player death particles are not assigned"); return; }
        var particles = Instantiate(deathParticles, _rb.position, Quaternion.identity);
        Destroy(particles.gameObject, 3f);
    }

    public void ApplyHeal(int amount)
    {
        Health += amount;
    }
}
