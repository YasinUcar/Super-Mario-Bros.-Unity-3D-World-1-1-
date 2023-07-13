using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCollisionController : MonoBehaviour
{
    private MarioDie _marioDie;

    private void Awake()
    {
        _marioDie = GetComponent<MarioDie>();
    }
    #region ENEMYS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("TurtleEnemy"))
        {

            _marioDie.ControlCurrentMarioStatus();
            //if (collision.gameObject.GetComponent<FollowPath>() != null)
            //    collision.gameObject.GetComponent<FollowPath>().enabled = false;
            //else
            //    collision.gameObject.GetComponent<OrangeEnemyPath>().enabled = false;
            //collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        }

        else if (collision.gameObject.CompareTag("TurtleShell"))
        {
            
            collision.transform.parent.GetComponent<TurtleEnemyController>().Movement(Mathf.Sign(transform.localScale.x));
        }
        #endregion
    }
}



