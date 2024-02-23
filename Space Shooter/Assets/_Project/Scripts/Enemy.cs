using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private int damageAmount;
    [SerializeField] private float movementSmoothing;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private int health = 100;

    private Rigidbody2D _targetBody;
    private Vector2 _moveDir;
    private Vector2 _newMoveDir;
    private float _rotation;
    private Quaternion _newRotation;

    private Rigidbody2D _rb;
    private Transform _transform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        MoveTowardsTarget();
        LookAtPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var isPlayer = collision.gameObject.TryGetComponent(out Player player);
        if (isPlayer)
        {
            player.TakeDamage(damageAmount);
        }
    }

    private void MoveTowardsTarget()
    {
        if (_targetBody == null) { Debug.LogWarning("There is no target to follow"); return; }
        if (_targetBody.gameObject.activeInHierarchy)
        {
            _moveDir = _targetBody.position - _rb.position;
            _rb.velocity = _moveDir.normalized * walkSpeed;
        }
        else
        {
            _newMoveDir = _rb.position - _targetBody.position;
            _moveDir = Vector2.Lerp(_moveDir, _newMoveDir, movementSmoothing);
            _rb.velocity = _moveDir.normalized * walkSpeed / 2;
        }
    }

    private void LookAtPlayer()
    {
        _rotation = MathF.Atan2(_moveDir.y, _moveDir.x) * Mathf.Rad2Deg;
        _newRotation = Quaternion.Euler(0, 0, _rotation - 90);
        _transform.rotation = Quaternion.Lerp(_transform.rotation, _newRotation, movementSmoothing);
    }

    public void GiveTarget(Rigidbody2D target)
    {
        _targetBody = target;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        GameEvents.Instance.InvokeEnemyKilledEvent();
        Destroy(gameObject);
        if (deathParticles == null) { Debug.LogWarning("Enemy death particles are not assigned"); return; }
        var particles = Instantiate(deathParticles, _rb.position, Quaternion.identity);
        Destroy(particles.gameObject, 3f);
    }
}
