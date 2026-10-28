using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(DinoStats))]
[RequireComponent(typeof(DinoMovement))]
public class DinoController : MonoBehaviour
{

    [SerializeField]
    private DinoConfig config;

    public static readonly List<DinoController> All = new List<DinoController>();
    public DinoConfig Config => config;
    public DinoStats Stats { get; private set;}
    public DinoMovement Movement { get; private set;}
    public Animator Animator { get; private set;}
    public string CurrentStateName => stateMachine?.CurrentState?.GetType().Name ?? "None";

    private DinoStateMachine stateMachine;

    private void OnEnable() => All.Add(this);
    private void OnDisable() => All.Remove(this);

    private void Awake()
    {
        Stats = GetComponent<DinoStats>();
        Movement = GetComponent<DinoMovement>();
        Animator = GetComponent<Animator>();

        Stats.Initialize(config);
        Movement.Initialize(config);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        stateMachine = new DinoStateMachine(this, new Idlestate());
    }

    // Update is called once per frame
    private void Update()
    {
        if (Stats.IsDead)
            return;
        stateMachine.Tick();
    }

    private void OnDrawGizmosSelected()
    {
        if (config == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, config.senseRange);
    }

    public bool TryGetHerdCentre(out Vector3 centre)
    {
        centre = Vector3.zero;
        int count = 0;
        float r2 = config.herdRadius * config.herdRadius;

        foreach (var other in All)
        {
            if (other == this || other.config != config)
                continue;

            if ((other.transform.position - transform.position).sqrMagnitude > r2)
                continue;

            centre += other.transform.position;
            count++;
        }

        if (count == 0)
            return false;

        centre /= count;
        return true;
    }
}
