using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
    [SerializeField]
    private GameSettings gameSettings;

    private float boomTimer;

    public Transform target;

    private void Start()
    {
        boomTimer = gameSettings.BoomTimer;

        Vector3 dir = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void Update()
    {
        if (target == null)
        {
            Explosion();

            return;
        }

        if (transform.position.y < -0.2F)
        {
            Explosion();
        }

        boomTimer -= Time.deltaTime;
        if (boomTimer < 0)
        {
            Explosion();
        }

        transform.Translate(transform.forward * gameSettings.ProjectileSpeed * Time.deltaTime * 2, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {            
            other.GetComponent<TargetController>().CountHits++;

            if (other.transform.tag == "Cube")
            {
                other.GetComponent<Animator>().Play("Base Layer.cube-scale-animation", 0, 0.25f);

                Explosion();
            } else if(other.transform.tag == "Sphere") 
            {
                Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                other.GetComponent<Renderer>().material.color = newColor;

                Explosion();
            } else if(other.transform.tag == "Cone") 
            {
                other.GetComponent<TargetController>().Swap();

                Explosion();
            }
        }
    }

    public void Explosion()
    {
        Instantiate(gameSettings.Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
