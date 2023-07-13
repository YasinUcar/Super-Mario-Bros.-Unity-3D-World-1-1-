using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] AnimationManager _sfxAudioSource;
    public static AnimationManager _instance;
    public static AnimationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AnimationManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("Animation Manager").AddComponent<AnimationManager>();
                }

            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        DontDestroyOnLoad(this);

    }
    public void EnableAnimator(GameObject animator)
    {
        animator.gameObject.SetActive(true);
    }
    public void TriggerAnim(GameObject outGameObject,string triggerName)
    {
       outGameObject.GetComponent<Animator>().SetTrigger(triggerName);
    }
}
