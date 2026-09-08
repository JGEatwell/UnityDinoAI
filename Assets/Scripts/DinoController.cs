using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(DinoStats))]
[RequireComponent(typeof(DinoMovement))]
public class DinoController : MonoBehaviour
{

    [SerializeField]
    private DinoConfig config;

    public DinoConfig Config => config;
    public DinoStats Stats { get; private set;}
    public DinoMovement Movement { get; private set;}
    public Animator Animator { get; private set;}

    private DinoStateMachine stateMachine;

    private void Awake()
    {
        Stats = GetComponent<DinoStats>();
        Movement = GetComponent<DinoMovement>();
        Animator = GetComponent<Animator>();

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

    private void GizmosSelected()
    {
        if (config == null)
            return;
        Gizmos.color = new Color(1f, 1f, 0f, 0.25f);
        Gizmos.DrawWireSphere(transform.position, config.senseRange);
    }
}
