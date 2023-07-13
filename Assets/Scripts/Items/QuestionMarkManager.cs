using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMarkManager : MonoBehaviour
{
    private UIManager _uIManager;
    private Animator _animator;
   
    void Start()
    {
        _animator = GetComponent<Animator>();
        _uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    public  void Enabled()
    {
        _animator.SetTrigger("PlayCoin");
    }

    
}
