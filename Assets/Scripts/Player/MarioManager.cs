using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarioManager : MonoBehaviour
{
    public static Action GameStarted;
    [SerializeField] private ChangeMarioSprite _changeMarioSprite;
    [SerializeField] private ChangeMarioCollider _changeMarioCollider;
    private bool _isBig=false;
    private bool _isJump;
    private bool _isFireFlower;
    private bool _isDownKey;
    private bool _isDeath;

    [SerializeField] private TextMeshProUGUI _healthText;
    private int _health = 3;
    public int CurrentHealthStatus()
    {
        _healthText.text = "x   " + _health.ToString();
        return _health;
    }
    public void ChangeHealth(int health)
    {
        _health = health;
        CurrentHealthStatus();
    }
    public void ChangeMarioSize(bool big)
    {
        _isBig = big;
    }
    public bool CurrentDeathStatus()
    {
        return _isDeath;
    }
    public void ChangeDeathStatus(bool death)
    {
        _isDeath = death;
    }
    public bool CurrentDownKey()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CurrentMarioSize()
    {
        return _isBig;
    }
    public void ChangeJumpStatus(bool jump)
    {
        _isJump= jump;
    }
    public bool CurrentJumpStatus()
    {
        return _isJump;
    }
    public void ChangeFireFlowerStatus(bool fireFlower)
    {
        _isFireFlower = fireFlower;
    }
    public bool CurrentFireFlowerStatus()
    {
        return _isFireFlower;
    }
    ///FOR DEVELOPMENT : 
    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 100, 100), "Small Mario"))
    //    {
    //        _isBig = false;
    //        _changeMarioSprite.ChangeCurrentMarioSprite("Normal");
    //        _changeMarioCollider.ChangeCurrentMarioCollider("Small");

    //    }
    //    if (GUI.Button(new Rect(130, 10, 100, 100), "Big Mario"))
    //    {
    //        _isBig = true;
    //        _changeMarioSprite.ChangeCurrentMarioSprite("Normal");
    //        _changeMarioCollider.ChangeCurrentMarioCollider("Big");
    //    }

    //    if (GUI.Button(new Rect(250, 10, 100, 100), "Fire Ball Mario"))
    //    {
    //        _isBig = true;
    //        ChangeFireFlowerStatus(true);   
    //        _changeMarioSprite.ChangeCurrentMarioSprite("Normal");
    //        _changeMarioCollider.ChangeCurrentMarioCollider("Big");
    //    }
    //}
}

