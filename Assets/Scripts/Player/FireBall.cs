using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private GameObject _fireball;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private Vector2 offset = new Vector2(0.4f, 0.1f);
    [SerializeField] private float _coolDownFb = 1f;
    [SerializeField] private AudioClip _fireballSFX;
    private bool _canShoot = true;
    private MarioManager _marioManager;
    private void Awake()
    {
        _marioManager = GameObject.FindGameObjectWithTag("MarioManager").GetComponent<MarioManager>();
    }    
    void Update()
    {
        if (_marioManager.CurrentFireFlowerStatus())
        {
            if (Input.GetButtonDown("Fire1") && _canShoot)
            {
                //localScale: -1 or 1 for changing sprite transform
                GameObject fireBallTemp = Instantiate(_fireball, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
                fireBallTemp.GetComponent<Rigidbody2D>().velocity = new Vector2(_velocity.x * transform.localScale.x, _velocity.y);
                CanShoot();
                AudioManager.Instance.PlaySound(_fireballSFX);
            }
        }
    }
    IEnumerator CanShoot()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_coolDownFb);
        _canShoot = true;
    }

}
