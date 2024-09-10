using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyBehaviour : MonoBehaviour
{

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _sightRange = 5f;
    [SerializeField] private float walkSpeed = 5f;
    Vector3 movement;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CanSeePlayer()){
            ChasePlayer();
        }/*
        else{
            Patrol();
        }*/
    }

    public bool CanSeePlayer(){

        Vector3 directionToPlayer = _playerTransform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        if (distanceToPlayer <= _sightRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, _sightRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ChasePlayer(){

        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        // We don't want him to go up like in 2D
        directionToPlayer.y = 0;
        transform.position += directionToPlayer * walkSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
    }
/*    public void Patrol(){

     
    }*/

}
