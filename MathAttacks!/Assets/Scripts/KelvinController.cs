using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelvinController : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;
    
    static bool created = false;

    void Start()
    {
        FinishController.OnDieAction += DoJump;
    }

    void Awake()
    {
        if (!created) 
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        } else 
        {
            Destroy (this.gameObject);
        }
    }

    void DoJump()
    {
        GetComponent<Animator>().SetTrigger("JumpTrigger");
    }
}
