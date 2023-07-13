using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private Transform[] _pathLocations;
    [SerializeField] private float _pathSpeed;
    int firstIndex = 0;
    int lastIndex = 0;
    private void Update()
    {
        Run();
    }
    public void Run()
    {
        if (firstIndex != _pathLocations.Length)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _pathLocations[firstIndex].position, _pathSpeed * Time.deltaTime);
            if (transform.position == _pathLocations[firstIndex].position)
            {
                firstIndex++;
            }
        }
        if (firstIndex == _pathLocations.Length)
        {
          
            transform.position = Vector2.MoveTowards(this.transform.position, _pathLocations[lastIndex].position, _pathSpeed * Time.deltaTime);
            if (transform.position == _pathLocations[lastIndex].position)
            {
                firstIndex--;
            }
        }
    }
}
