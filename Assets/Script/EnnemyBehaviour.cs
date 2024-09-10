using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyBehaviour : MonoBehaviour
{

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _sightRange = 5f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float _attackCooldown = 5f;
    private bool _canAttack = true;
    private bool _playerInRange = false;
    Vector3 movement;
    [SerializeField] Animator _enemyAnim;



    // Start is called before the first frame update
    void Start()
    {
        _enemyAnim = GetComponent<Animator>();
        _enemyAnim.SetBool("AIRunning",false);
        _enemyAnim.SetBool("IsAttacking",false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CanSeePlayer()){
            _enemyAnim.SetBool("AIRunning",true);
            ChasePlayer();
        }
        else{
            _enemyAnim.SetBool("AIRunning",false);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            Debug.Log("Player entered trigger");
            if (_canAttack)
            {
                _enemyAnim.SetBool("IsAttacking", true);
                LifeManager.instance.ReduceLife(1);
                StartCoroutine(AttackCooldownCoroutine());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
            _enemyAnim.SetBool("IsAttacking", false);
            Debug.Log("Player exited trigger");
        }
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
    
}