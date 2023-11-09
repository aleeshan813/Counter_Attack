using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public static AnimationManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBool_Anim(string name, bool Setbool)
    {
        animator.SetBool(name, Setbool);
    }

    public void Set_Trigger_Anim(string trigger)
    { 
        animator.SetTrigger(trigger);
    }

    public void Reset_Trigger_Anim(string trigger) 
    {
        animator.ResetTrigger(trigger);
    }
}
