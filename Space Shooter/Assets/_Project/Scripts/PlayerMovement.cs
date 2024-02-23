using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private Rigidbody2D _rigidbody;

    #region скрипт рывка 7
    private Collider2D _collider;

    private bool _isDashing = false;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private GameObject dashEffect;
    #endregion

    private void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();

        #region скрипт рывка 8
        _collider = this.GetComponent<Collider2D>();
        #endregion
    }

    private void FixedUpdate()
    {
        #region скрипт рывка 10
        if (_isDashing) return;
        #endregion

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector2 moveDirection = new Vector2(horizontal, vertical);
        _rigidbody.velocity = moveDirection.normalized * MoveSpeed;
    }

    #region скрипт рывка 10
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isDashing || _rigidbody.velocity == Vector2.zero) return;
            Vector2 dashDirection = _rigidbody.velocity.normalized;
            StartCoroutine(PerformDash(dashDirection));
        }
    }
    #endregion

    #region скрипт рывка 9
    private IEnumerator PerformDash(Vector2 direction)
    {
        
        #region СКРИПТ РЫВКА 15
        SoundSystem.Instance.PlayDashSound();
        #endregion
        _isDashing = true;

        Player.ShouldRotate = false;
        _collider.isTrigger = true;

        _rigidbody.velocity = direction * dashSpeed;
        RotateAndSpawnEffects();
        yield return new WaitForSeconds(dashTime);
        _rigidbody.velocity = Vector2.zero;

        Player.ShouldRotate = true;
        _collider.isTrigger = false;
        _isDashing = false;
    }
    #endregion

    #region скрипт рывка 11
    private void RotateAndSpawnEffects()
    {
        Vector3 lookDirection = _rigidbody.velocity - _rigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rigidbody.rotation = angle;
        StartCoroutine(SpawnEffects());
    }

    private IEnumerator SpawnEffects()
    {
        float elapsedTime = 0;
        while (elapsedTime <= dashTime && dashEffect != null)
        {
            elapsedTime += Time.deltaTime;
            var dashFx = Instantiate(dashEffect, transform.position, transform.rotation);
            dashFx.transform.Rotate(0, 0, 90);
            Destroy(dashFx, dashTime);
            yield return null;
        }
    }
    #endregion
}