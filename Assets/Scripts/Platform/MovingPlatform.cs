using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float checkDistance = 0.05f;

    private Transform _targetWaypoint;
    private int _currentWaypointIndex;
    private void Start()
    {
        _targetWaypoint = waypoints[0];
    }
    
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            _targetWaypoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _targetWaypoint.position) < checkDistance)
        {
            _targetWaypoint = GetNextWayPoint();
        }
    }

    private Transform GetNextWayPoint()
    {
        _currentWaypointIndex++;
        if (_currentWaypointIndex >= waypoints.Length)
        {
            _currentWaypointIndex = 0;
        }

        return waypoints[_currentWaypointIndex];
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var platformMovement = col.collider.GetComponent<Hero.HeroFiniteStateMachine.Hero>();
        if (platformMovement != null)
        {
            platformMovement.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        var platformMovement = other.collider.GetComponent<Hero.HeroFiniteStateMachine.Hero>();
        if (platformMovement != null)
        {
            platformMovement.ResetParent();
        }
    }
}
