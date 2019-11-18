
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController:Enemy
{
    public List<Transform> targets;
    public float lookRadius = 10f;
    public float explosionRadius;
    public float explosionForce;

    Rigidbody2D _rb2d;
    Vector3 _origPos;
    NavMeshAgent _agent;
    NavMeshObstacle obsticle;

    enum AnimationStates
    {
        Default,
        WalkUp,
        WalkDown,
        WalkLeft,
        WalkRight
    }

    void Start()
    {
        _origPos = transform.position;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        obsticle = GetComponent<NavMeshObstacle>();

        foreach(var player in GameObject.FindObjectsOfType<Player>())
        {
            targets.Add(player.transform);
        }

        //obsticle.
        //_agent.autoRepath = true;

        //InvokeRepeating("CheckPlayerPos", 1.0f, 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Vector3 explosionPos = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, 5);
        foreach (Collider2D hit in colliders)
        {
            if (!hit.CompareTag("Player")) continue;

            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            //if (rb != null)
                //rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, 3.0F);
        }
    }

    private void Update()
    {
        if (targets.Count <= 0)
        {
            foreach (var player in GameObject.FindObjectsOfType<Player>())
            {
                targets.Add(player.transform);
            }
            return;
        }

        Transform closestTarget = null;
        float closestDistance = lookRadius;

        foreach (var target in targets)
        {
            float distance = Vector2.Distance(target.position, transform.position);

            if(distance <= lookRadius && distance < closestDistance)
            {
                closestTarget = target;
                closestDistance = distance; 
            }
        }

        if(closestTarget != null)
        {
            _agent.isStopped = false;
            _agent.SetDestination(closestTarget.position);
        }
        else
        {
            if(_agent.isActiveAndEnabled)
                _agent.isStopped = true;
        }

        Vector3 moveDirection = gameObject.transform.position - _origPos;
        if(moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            AnimationStates cState = AnimationStates.Default;
            switch (lookRotation.x)
            {
                case -1:
                    cState = AnimationStates.WalkLeft;
                    break;
                case 1:
                    cState = AnimationStates.WalkRight;
                    break;

            }

            Debug.Log(cState.ToString());
        }
        _origPos = transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
