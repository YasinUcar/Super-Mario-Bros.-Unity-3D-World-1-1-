using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMarioCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _marioCollider;
   
    [SerializeField] private BoxCollider2D _marioLegsCollider;
    public void ChangeCurrentMarioCollider(string currentCollider)
    {
        switch (currentCollider)
        {
            case "Big":
                _marioCollider.size = new Vector2(1.066667f, 1.46f);
                _marioCollider.offset = new Vector2(0, 0.33f);

                _marioLegsCollider.size = new Vector2(1, 0.21f);
                _marioLegsCollider.offset = new Vector2(0, -0.49f);
                break;
            case "Small":
                _marioCollider.size = new Vector2(0.8666667f, 0.7316253f);
                _marioCollider.offset = new Vector2(0, 0.1675206f);

                _marioLegsCollider.size = new Vector2(0.7163858f, 0.1459908f);
                _marioLegsCollider.offset = new Vector2(-0.02825785f, -0.01418781f);
                break;
            case "Down":
                _marioCollider.size = new Vector2(1.019423f, 1.234818f);
                _marioCollider.offset = new Vector2(-0.00449276f, 0.1136072f);

                _marioLegsCollider.size = new Vector2(1.04506f, 0.09876919f);
                _marioLegsCollider.offset = new Vector2(0.0005640984f, -0.1858439f);
                break;
            default:
                break;
        }
    }
    IEnumerator WaitNewCollider()
    {
        _marioCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        
    }
}
