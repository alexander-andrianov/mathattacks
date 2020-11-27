using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;

    private GameObject cone;
    private GameObject cylinder;

    private int countHits = 0;

    public delegate void ScoreUpdatedAction();
    public static event ScoreUpdatedAction OnScoreUpdated;

    public int CountHits
    {
        get { return countHits; }
        set { countHits = value; }
    }

    void Start()
    {
        if(gameObject.tag == "Cone")
        {
            cone = transform.GetChild(0).gameObject;
            cylinder = transform.GetChild(1).gameObject;
        }
    }

    void Update()
    {
        if(countHits >= 3)
        {
            gameSettings.CurrentScore++;
            OnScoreUpdated();

            Instantiate(gameSettings.Explosion, transform.position, transform.rotation);
            Destroy(gameObject);

            return;
        }

        transform.Translate(Vector3.left * gameSettings.Speed * Time.deltaTime, Space.World);
    }

    public void Swap()
    {
        if(cone.activeSelf)
        {
            cone.SetActive(false);
            cylinder.SetActive(true);
        } else
        {
            cone.SetActive(true);
            cylinder.SetActive(false);
        }
    }
}
