using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour { 
    [SerializeField]
    private GameSettings gameSettings;

    private float timer;
    private GameObject currentTarget;
    private Transform lockOnPos;
    private Transform muzzleSub;
    private Animator animator;
    private Vector3 randomRot;

    public Transform turreyHead;
    public Transform muzzleMain;

    void Start() {
        gameSettings.CurrentScore = 0;

        InvokeRepeating("CheckForTarget", 0, 0.5f);

        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }
	
	void Update() {
        if (currentTarget != null)
        {
            FollowTarget();

            float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (currentTargetDist > gameSettings.AttackDistance)
            {
                currentTarget = null;
            }
        }
        else
        {
            IdleRotate();
        }

        timer += Time.deltaTime;
        if (timer >= gameSettings.ShootCoolDown)
        {
            if (currentTarget != null)
            {
                timer = 0;
                
                if (animator != null)
                {
                    animator.SetTrigger("Fire");
                    ShootTrigger();
                }
                else
                {
                    ShootTrigger();
                }
            }
        }
	}

    private void CheckForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, gameSettings.AttackDistance);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Cube" || colls[i].tag == "Sphere" || colls[i].tag == "Cone")
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    private void FollowTarget()
    {
        /*Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
        turreyHead.forward = targetDir;*/

        Vector3 direction = currentTarget.transform.position - turreyHead.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(turreyHead.rotation, lookRotation, Time.deltaTime * gameSettings.TurnSpeed).eulerAngles;
        turreyHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void ShootTrigger()
    {
        Shoot(currentTarget);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gameSettings.AttackDistance);
    }

    public void IdleRotate()
    {
        bool refreshRandom = false;
        
        if (turreyHead.rotation != Quaternion.Euler(randomRot))
        {
            turreyHead.rotation = Quaternion.RotateTowards(turreyHead.transform.rotation, Quaternion.Euler(randomRot), gameSettings.LookSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {

                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    public void Shoot(GameObject go)
    {
        //Instantiate(gameSettings.ShootingEffect, muzzleMain.transform.position, muzzleMain.rotation);
        ObjectPoolScript.Spawn(gameSettings.ShootingEffect, muzzleMain.transform.position, muzzleMain.rotation);
        //GameObject missleGo = Instantiate(gameSettings.Bullet, muzzleMain.transform.position, muzzleMain.rotation);
        GameObject missleGo = ObjectPoolScript.Spawn(gameSettings.Bullet, muzzleMain.transform.position, muzzleMain.rotation);
        Projectile projectile = missleGo.GetComponent<Projectile>();
        projectile.target = currentTarget.transform;
    }
}
