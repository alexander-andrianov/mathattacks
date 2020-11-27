using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;

    private int counter;

    public delegate void ObjectDieAction();
    public static event ObjectDieAction OnDieAction;

    void Start()
    {
        counter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if (other.transform.tag == "Cube" || other.transform.tag == "Sphere" ||other.transform.tag == "Cone")
            {
                ExplodeTarget(other);

                counter++;
            }

            if(counter >= gameSettings.GameoverLimit)
            {
                GameObject.FindWithTag("Kelvin").GetComponent<Animator>().SetTrigger("JumpTrigger");
                GameObject.FindWithTag("UI").transform.GetChild(0).gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ExplodeTarget(Collider target)
    {
        OnDieAction();

        Instantiate(gameSettings.Explosion, target.transform.position, target.transform.rotation);
        ObjectPoolScript.Despawn(target.gameObject);
    }
}
