using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public BulletScript bulletPrefab;
    private Transform _transform;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameEvents.Instance.InvokePlayerShootEvent();
            var spawnedBullet = Instantiate(bulletPrefab, _transform.position, _transform.rotation);
            Destroy(spawnedBullet.gameObject, 5f);
        }
    }
}
