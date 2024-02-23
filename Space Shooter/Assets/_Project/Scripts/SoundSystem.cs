using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] private List<AudioClip> playerShootingClips;
    private List<AudioSource> _shootingSounds = new List<AudioSource>();

    #region яхярелю гбсйнб 17 /////////////////////////////////////////////////////////////////////////////////////////////

    [SerializeField] private AudioClip enemyExplosionClip;
    private AudioSource _enemyExplosionAudioSource;

    [SerializeField] private AudioClip playerExplosionClip;
    private AudioSource _playerExplosionAudioSource;
    #endregion
    #region яхярелю гбсйнб 24 дг /////////////////////////////////////////////////////////////////////////////////////////////

    [SerializeField] private AudioClip specialAbilityClip;
    private AudioSource _specialAbilitySource;
    #endregion
    void Start()
    {
        foreach (var clip in playerShootingClips)
        {
            _shootingSounds.Add(ConvertClipToComponent(clip));
        }

        #region яхярелю гбсйнб 17

        _enemyExplosionAudioSource = ConvertClipToComponent(enemyExplosionClip);
        _playerExplosionAudioSource = ConvertClipToComponent(playerExplosionClip);

        #region яхярелю гбсйнб 24 дг 

        _specialAbilitySource = ConvertClipToComponent(specialAbilityClip);
        #endregion
        #endregion 
    }

    #region яхярелю гбсйнб 15
    private void OnEnable()
    {
        GameEvents.Instance.OnPlayerShoot += PlayRandomShootSound;

        #region яхярелю гбсйнб 17 

        GameEvents.Instance.OnPlayerKilled += PlayPlayerExplosionSound;
        GameEvents.Instance.OnEnemyKilled += PlayEnemyExplosionSound;
        #endregion
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnPlayerShoot -= PlayRandomShootSound;

        #region яхярелю гбсйнб 17

        GameEvents.Instance.OnPlayerKilled -= PlayPlayerExplosionSound;
        GameEvents.Instance.OnEnemyKilled -= PlayEnemyExplosionSound;
        #endregion
    }

    #region яхярелю гбсйнб 19
    private void PlayPlayerExplosionSound()
    {
        _playerExplosionAudioSource.volume = 0.5f;
        _playerExplosionAudioSource.PlayOneShot(playerExplosionClip);
    }
    private void PlayEnemyExplosionSound()
    {
        _enemyExplosionAudioSource.volume = 0.5f;
        _enemyExplosionAudioSource.PlayOneShot(enemyExplosionClip);
    }
    #endregion

    private void PlayRandomShootSound()
    {
        int randomIndex = Random.Range(0, _shootingSounds.Count);
        AudioSource randomSource = _shootingSounds[randomIndex];
        randomSource.PlayOneShot(randomSource.clip);
    }
    #endregion

    private AudioSource ConvertClipToComponent(AudioClip clipToConvert)
    {
        AudioSource shootingSource = gameObject.AddComponent<AudioSource>();
        shootingSource.clip = clipToConvert;
        shootingSource.playOnAwake = false;
        shootingSource.volume = 0.05f;
        return shootingSource;
    }

    #region яхярелю гбсйнб 12
    public static SoundSystem Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    #region яйпхор пшбйю 14
    public void PlayDashSound()
    {
        _specialAbilitySource.PlayOneShot(specialAbilityClip);
    }
    #endregion
}

