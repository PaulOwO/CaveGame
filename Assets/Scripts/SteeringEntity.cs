
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SteeringEntity : MonoBehaviour
{
    public enum SteeringType
    {
        DeltaPosByHand,
        VelocityByHand,
        UsingConstantForce,
        UsingSteeringForce,
        WanderSteeringForce
    }
    [SerializeField] private Transform pathParent;
    private Vector3[] path;
    private int targetIndex = 0;
    private Transform EnemyChaser;
    private Rigidbody2D body;

    [SerializeField] private float entitySpeed = 3.0f;
    [SerializeField] private float pointThreshold = 0.5f;
    [SerializeField] private float forceIntensity = 5.0f;
    [SerializeField] private float maxSteeringForce = 100.0f;
    [SerializeField] private SteeringType steeringType = SteeringType.VelocityByHand;
    [SerializeField] private float wanderAngle = 10.0f;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        EnemyChaser = transform;
        path = new Vector3[pathParent.childCount];
        if(steeringType == SteeringType.DeltaPosByHand)
            Destroy(body);
    }

    private void Update()
    {
        for (int i = 0; i < pathParent.childCount; i++)
        {
            path[i] = pathParent.GetChild(i).position;
        }
        EnemyChaser = transform;
        if (steeringType == SteeringType.DeltaPosByHand)
        {
            var entityPosition = EnemyChaser.position;
            var targetPosition = path[targetIndex];
            var deltaPos = targetPosition - entityPosition;
            if (deltaPos.magnitude < pointThreshold)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                    targetIndex = 0;
            }
            else
            {
                entityPosition += entitySpeed * Time.deltaTime * deltaPos.normalized;
                EnemyChaser.position = entityPosition;
            }
        }
    }

    private void FixedUpdate()
    {
        if (steeringType == SteeringType.DeltaPosByHand) return;
        var entityPosition = EnemyChaser.position;
        var targetPosition = path[targetIndex];
        var deltaPos = targetPosition - entityPosition;
        if (deltaPos.magnitude < pointThreshold)
        {
            targetIndex++;
            if (targetIndex >= path.Length)
                targetIndex = 0;
        }
        else
        {
            switch (steeringType)
            {
                case SteeringType.VelocityByHand:
                    body.velocity = entitySpeed * deltaPos.normalized;
                    break;
                case SteeringType.UsingConstantForce:
                    body.AddForce(deltaPos.normalized, ForceMode2D.Force);
                    break;
                case SteeringType.UsingSteeringForce:
                {
                    Vector2 targetVelocity = entitySpeed * deltaPos.normalized;
                    var currentVelocity = body.velocity;
                    var deltaVelocity = targetVelocity - currentVelocity;
                    var force = body.mass * deltaVelocity / Time.fixedDeltaTime;
                    if (force.magnitude > maxSteeringForce)
                    {
                        force = force.normalized * maxSteeringForce;
                    }

                    body.AddForce(force);
                    break;
                }
                case SteeringType.WanderSteeringForce:
                {
                    Vector2 targetVelocity = entitySpeed * deltaPos.normalized;
                    targetVelocity = Quaternion.AngleAxis(
                        UnityEngine.Random.Range(-wanderAngle, wanderAngle), Vector3.back) * targetVelocity;
                    var currentVelocity = body.velocity;
                    var deltaVelocity = targetVelocity - currentVelocity;
                    var force = body.mass * deltaVelocity / Time.fixedDeltaTime;
                    if (force.magnitude > maxSteeringForce)
                    {
                        force = force.normalized * maxSteeringForce;
                    }

                    body.AddForce(force);
                    break;
                }
            }
        }
    }
}
