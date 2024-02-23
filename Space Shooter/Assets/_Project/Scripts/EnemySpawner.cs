using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Text counter;

    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private int enemiesPerWave;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float waveInetrval;
    private Transform _transform;
    private Enemy _enemy;
    private int _score;

    private int _enemyCount;

    public int EnemyCount
    {
        get => _enemyCount;
        set => _enemyCount = value;
    }


    private void Awake()
    {
        _transform = GetComponent<Transform>();

        if (enemyPrefabs.Count <= 0) 
        { 
            Debug.LogWarning("There are no enemies to spawn. Assign at least one enemy prefab in EnemySpawner"); 
            return;
        }
        StartCoroutine(SpawnEnemies());
    }

    private void OnEnable()
    {
        GameEvents.Instance.OnEnemyKilled += AddScore;
        GameEvents.Instance.OnEnemyKilled += DecrementEnemyCount;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnEnemyKilled -= AddScore;
        GameEvents.Instance.OnEnemyKilled -= DecrementEnemyCount;
    }

    private void AddScore()
    {
        _score++;
        if (counter == null) return;
        counter.text = _score.ToString();
    }

    private void DecrementEnemyCount()
    {
        _enemyCount--;
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (EnemyCount <= 0)
            {
                float randDirection;
                float randDistance;
                int randEnemy;
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    randDirection = Random.Range(0, 360);
                    randDistance = Random.Range(10, 25);
                    randEnemy = Random.Range(0, enemyPrefabs.Count);
                    _enemy = enemyPrefabs[randEnemy];

                    float posX = _transform.position.x + (Mathf.Cos(randDirection * Mathf.Deg2Rad) * randDistance);
                    float posY = Mathf.Abs(_transform.position.y + (Mathf.Sin(randDirection * Mathf.Deg2Rad) * randDistance));
                    var spawnedEnemy = Instantiate(_enemy, new Vector3(posX, posY, 0), Quaternion.identity);
                    spawnedEnemy.GiveTarget(playerBody);
                    EnemyCount++;
                    yield return new WaitForSeconds(spawnInterval);
                }
            }
            yield return new WaitForSeconds(waveInetrval);
        }
    }
}
