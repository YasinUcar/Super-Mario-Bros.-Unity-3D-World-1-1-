using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioDie : MonoBehaviour
{
    private MarioManager _marioManager;
    private ChangeMarioSprite _changeMarioSprite;
    private ChangeMarioCollider _changeMarioCollider;
    private CheckPointManager _checkPointManager;
    private void Awake()
    {
        _marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();
        _changeMarioCollider = GetComponent<ChangeMarioCollider>();
        _changeMarioSprite = GetComponent<ChangeMarioSprite>();
        _checkPointManager = GameObject.FindGameObjectWithTag("CheckPointManager").GetComponent<CheckPointManager>();
    }
    public void ControlCurrentMarioStatus()
    {
        if (_marioManager.CurrentFireFlowerStatus() || _marioManager.CurrentMarioSize())
        {
            if (_marioManager.CurrentFireFlowerStatus())
            {
                _marioManager.ChangeFireFlowerStatus(false);
                _marioManager.ChangeMarioSize(true);
                _changeMarioSprite.ChangeCurrentMarioSprite("Normal");
                _changeMarioCollider.ChangeCurrentMarioCollider("Big");
            }
            else if (_marioManager.CurrentMarioSize())
            {
                _marioManager.ChangeMarioSize(false);
                _changeMarioSprite.ChangeCurrentMarioSprite("Normal");
                _changeMarioCollider.ChangeCurrentMarioCollider("Small");
            }
        }
        else
            Die();
    }
    private void Die()
    {

        SceneManager.LoadScene(0);
        _marioManager.ChangeDeathStatus(true);
        _checkPointManager.CheckPointLocation("");
    }
}
