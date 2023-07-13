using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PowerUpManager : MonoBehaviour
{
    [SerializeField] AudioClip _collisionSFX;
    private MarioManager _marioManager;

    private Mushrooms _mushrooms;
    private FireFlower _fireFlower;
    private void Awake()
    {
        _mushrooms = GetComponentInChildren<Mushrooms>();
        _fireFlower = GetComponentInChildren<FireFlower>();
        _marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();
    }
    public void ChangePower()
    {
        if (_marioManager.CurrentMarioSize())
        {
            if (_fireFlower != null)
                _fireFlower.Movement();
        }
        else
            _mushrooms.Movement();
        //CollisionSFX();
        GetComponent<PowerUpManager>().enabled = false;
    }
    public void CollisionSFX()
    {
        AudioManager.Instance.PlaySound(_collisionSFX);
    }
   
}
