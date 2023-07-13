using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Rigidbody2D _rigidbody2D;
    int isRight = 0;
  //  bool isRight=true;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        _rigidbody2D.velocity = isRight%2==0 ? Vector3.left * _movementSpeed * Time.deltaTime : Vector3.right * _movementSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            isRight++;
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            isRight++;
        }

    }

}
