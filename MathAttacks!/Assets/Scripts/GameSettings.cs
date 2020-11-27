using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [Header("MainAttributes")]
    [SerializeField]
    private int gameoverLimit;
    [SerializeField]
    private int winLimit;

    [Header("CommonTargetAttributes")]
    [SerializeField]
    private float speed;

    [Header("Cube")]
    [SerializeField]
    private float cubeSpawnDelay;
    [SerializeField]
    private GameObject cubePrefab;

    [Header("Sphere")]
    [SerializeField]
    private float sphereSpawnDelay;
    [SerializeField]
    private GameObject spherePrefab;

    [Header("Cone")]
    [SerializeField]
    private float coneSpawnDelay;
    [SerializeField]
    private GameObject conePrefab;

    [Header("Turret")]
    [SerializeField]
    private float shootCoolDown;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float lookSpeed;
    [SerializeField]
    private float turretTurnSpeed;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject shootingEffect;

    [Header("Projectile")]
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float boomTimer;
    [SerializeField]
    private ParticleSystem explosion;

    [Header("Score")]
    [SerializeField]
    private int currentScore;

    public int GameoverLimit
    {
        get
        {
            return gameoverLimit;
        }
    }

    public int WinLimit
    {
        get
        {
            return winLimit;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
    }
    
    public float CubeSpawnDelay
    {
        get
        {
            return cubeSpawnDelay;
        }
    }

    public float SphereSpawnDelay
    {
        get
        {
            return sphereSpawnDelay;
        }
    }

    public float ConeSpawnDelay
    {
        get
        {
            return coneSpawnDelay;
        }
    }

    public float ShootCoolDown
    {
        get
        {
            return shootCoolDown;
        }
    }

    public float AttackDistance
    {
        get
        {
            return attackDistance;
        }
    }

    public float LookSpeed
    {
        get
        {
            return lookSpeed;
        }
    }

    public float TurretTurnSpeed
    {
        get
        {
            return turretTurnSpeed;
        }
    }

    public GameObject Bullet
    {
        get
        {
            return bullet;
        }
    }

    public GameObject ShootingEffect
    {
        get
        {
            return shootingEffect;
        }
    }

    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }

    public float TurnSpeed
    {
        get
        {
            return turnSpeed;
        }
    }

    public float BoomTimer
    {
        get
        {
            return boomTimer;
        }
    }

    public ParticleSystem Explosion
    {
        get
        {
            return explosion;
        }
    }

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
        }
    }
}