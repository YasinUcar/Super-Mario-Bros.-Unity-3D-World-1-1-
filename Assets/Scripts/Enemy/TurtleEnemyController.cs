using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurtleEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _turtleShell;
    [SerializeField] private float _movementSpeed;
    [SerializeField] BoxCollider2D box1,box2;
    float _marioPos;
    bool isStart;
    private void Update()
    {
        if (isStart)
            Movement(_marioPos);
    }
    public void Controller()
    {
        this.gameObject.tag = "EnemyEmpty";
        GetComponent<BoxCollider2D>().isTrigger = true;

        GetComponent<SpriteRenderer>().enabled = false;
        
        GetComponent<Animator>().enabled = false;
        if (GetComponentInParent<EnemyPath>() != null)
            GetComponentInParent<EnemyPath>().enabled = false;
        _turtleShell.SetActive(true);

    }
    public void Movement(float marioPos)
    {
        isStart = true;
        _marioPos = marioPos;
        //   _turtleShell.GetComponent<Rigidbody2D>().velocity = marioPos == 1 ? Vector3.right * _movementSpeed * Time.deltaTime : Vector3.right * _movementSpeed * Time.deltaTime;
        //_turtleShell.GetComponent<Rigidbody2D>().AddForce(marioPos == 1 ? Vector2.right * _movementSpeed * Time.deltaTime : Vector2.left * _movementSpeed * Time.deltaTime, ForceMode2D.Force);
        _turtleShell.GetComponent<Rigidbody2D>().AddForce(marioPos == 1 ? Vector2.right * _movementSpeed * Time.deltaTime :  Vector2.left * _movementSpeed * Time.deltaTime, ForceMode2D.Force);

    }
 

}
