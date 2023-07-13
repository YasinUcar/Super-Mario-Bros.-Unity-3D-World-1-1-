using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class FireBallController : MonoBehaviour
{

    //code reference : https://youtu.be/4XrazhLqLSQ

    private Rigidbody2D _rigidbody2D;
    private Vector2 _velocity;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Explode(10);//if still available
        _velocity = _rigidbody2D.velocity;
    }
    private void Update()
    {
        if (_rigidbody2D.velocity.y < _velocity.y)
        {
            _rigidbody2D.velocity = _velocity;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        _rigidbody2D.velocity = new Vector2(_velocity.x, -_velocity.y);

        if (collision.contacts[0].normal.x != 0)
        {
            Explode();
        }
         if (collision.gameObject.CompareTag("Enemy"))
        {
         
            if (collision.gameObject.GetComponent<EnemyDie>() != null)
                collision.gameObject.GetComponent<EnemyDie>().Die();
            
            Explode();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
    
    void Explode(int explodeTime = 0)//optional argument default value = 0
    {
        Destroy(this.gameObject, explodeTime);
    }

}
