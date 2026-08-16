using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DinoMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private DinoConfig config;

    public bool HasArrived =>
        !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance &&
        (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f);
    
    public void Initialize(DinoConfig cfg)
    {
        config = cfg;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = config.walkSpeed;
        agent.angularSpeed = config.rotationSpeed;
    }

    public void SetSpeed(bool running)
    {
        agent.speed = running ? config.runSpeed : config.walkSpeed;
    
    }

    public void MoveTo(Vector3 destination)
    {
        if (agent.isOnNavMesh)
            agent.SetDestination(destination);
    }

    public void Stop()
    {
        if (agent.isOnNavMesh)
            agent.ResetPath();
    }
    
    public bool TryGetRandomPoint(Vector3 origin, float radius, out Vector3 result)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius + origin;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = origin;
        return false;
    }
}
