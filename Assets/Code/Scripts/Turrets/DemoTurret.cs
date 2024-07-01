using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DemoTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    //New Firing Bullets
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    //

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    //New Firing Bullets
    [SerializeField] private float bps = 1f; // bullets per second
    
    private float timeUntilFire;
    //
    private Transform target;
    
    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        //New Firing Bullets
        else
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1/bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation); // Fixed the orientation of the bullets 22.06
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

        //Debug.Log(gameObject.name + ": Shot Fired!");
    }
    //

    private void FindTarget()
    {
        //RaycastHit2D[] is an Array.
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            //hits[0] - the first element of the RaycastHit2D[] array.
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float targetingAngle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetingAngle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Make Rotation Instant
    }


    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }


    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
