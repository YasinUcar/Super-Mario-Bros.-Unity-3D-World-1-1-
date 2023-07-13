using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _checkPoints = new List<Transform>();
    [SerializeField] private Transform _mario;
    [SerializeField] private MarioManager _marioManager;
    public void CheckPointLocation(string checkPointName)
    {
        if (_marioManager.CurrentDeathStatus())
        {
            switch (checkPointName)
            {

                case "1":

                

                     //   _mario.transform.position = _checkPoints[0].transform.position;

                    break;
                case "2":
                    //_mario.transform.position = _checkPoints[2].transform.position;
                    break;


                default:
//                    _mario.transform.position = _checkPoints[0].transform.position;
                    break;
            }
        }
    }


}
