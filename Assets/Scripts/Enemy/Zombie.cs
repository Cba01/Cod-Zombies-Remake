using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using EZCameraShake;


public class Zombie : MonoBehaviour
{
    WaveManager waveManager;
    UIAnim UIAnim;
    NavMeshAgent navMeshAgent;
    Animator anim;
    MultiAimConstraint mac;
    PlayerStats playerStats;
    Rigidbody[] ragdollRigidbodies;



    public float health;
    public bool isDead = false;
    public bool canMove = true;
    public bool isAttacking = false;

    public Transform aimIK;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float damageMultiplier;
    [SerializeField]
    private int hitReward = 10;
    [SerializeField]
    private int killReward = 60;
    [SerializeField]
    private int headshotKillReward = 100;

    private float timeOfLastAttack;
    private bool hasStopped = false;

    void Start()
    {
        UIAnim = FindObjectOfType<UIAnim>();
        waveManager = FindObjectOfType<WaveManager>();
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerStats = FindObjectOfType<PlayerStats>();
        player = playerStats.transform;
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    void Update()
    {
        if (!isDead)
        {
            LookAtPlayer();

            if (canMove && !InAttackDistance() && !isAttacking)
            {
                FollowPlayer();
            }
            else if (canMove && InAttackDistance() && !isAttacking)
            {
                Attack();
            }
        }
    }
    private void LateUpdate()
    {
        isAttacking = false;

    }

    private void FollowPlayer()
    {
        if (navMeshAgent.isStopped)
        {
            navMeshAgent.isStopped = false;
        }

        navMeshAgent.speed = walkSpeed;
        anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
        navMeshAgent.SetDestination(player.position);
    }

    private void Attack()
    {
        if (Time.time >= timeOfLastAttack + attackSpeed)
        {
            timeOfLastAttack = Time.time;
            isAttacking = true;
            navMeshAgent.isStopped = true;
            anim.SetTrigger("isAttacking");
            playerStats.TakeDamage(10);
            CameraShaker.Instance.ShakeOnce(5, 1, 0.5f, 0.5f);

        }
    }

    private void LookAtPlayer()
    {
        aimIK.transform.position = player.position;
    }

    private bool InAttackDistance()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        //Redondear Ditancia entre el jugador y el enemigo
        distanceToPlayer = Mathf.Round(distanceToPlayer);

        if (distanceToPlayer <= navMeshAgent.stoppingDistance)
        {
            if (!hasStopped)
            {
                hasStopped = true;
                timeOfLastAttack = Time.time;
            }
            return true;
        }
        else
        {
            if (hasStopped)
            {
                hasStopped = false;
            }
            return false;
        }
    }

    public void TakeDamage(float damage, bool isHeadshot)
    {
        health -= damage;
        if (health <= 0)
        {
            if (!isDead)
            {
                if (isHeadshot)
                {
                    playerStats.balance += headshotKillReward;

                    isDead = true;
                    waveManager.remainingZombies--;
                    navMeshAgent.isStopped = true;
                    UIAnim.AddScore(headshotKillReward.ToString());
                    EnableRagdoll();

                }
                else
                {
                    playerStats.balance += killReward;

                    isDead = true;
                    waveManager.remainingZombies--;
                    UIAnim.AddScore(killReward.ToString());

                    navMeshAgent.isStopped = true;
                    EnableRagdoll();

                }
            }

        }
        else if (health > 0)
        {
            playerStats.balance += hitReward;
            UIAnim.AddScore(hitReward.ToString());

        }
    }



    private void DisableRagdoll()
    {
        foreach (var rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        anim.enabled = true;

    }
    private void EnableRagdoll()
    {
        foreach (var rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        anim.enabled = false;
    }

}
