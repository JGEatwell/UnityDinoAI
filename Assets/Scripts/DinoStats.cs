using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DinoController))]
public class DinoStats : MonoBehaviour
{
    [SerializeField]
    private DinoConfig config;
/*-----------------------------------------------------------------------*/
    public float Health { get; private set;}
    public float Hunger { get; private set;}
    public float Thirst { get; private set;}
    public float Tiredness { get; private set;}
/*-----------------------------------------------------------------------*/
   public bool IsHungry => Hunger <= config.hungerThreshhold;
   public bool IsThirsty => Thirst <= config.thirstThreshhold;
   public bool IsTired => Tiredness <= config.tiredThreshhold;
   public bool IsDead => Health <= 0f;

   public event Action OnDeath;

    private void Awake()
    {
        if (config == null)
            config = GetComponent<DinoController>()?.Config;

        Health = config.maxhealth;
        Hunger = 100f;
        Thirst = 100f;
        Tiredness = 100f;
    }


    // Update is called once per frame
    private void Update()
    {
        if (IsDead)
            return;

        Hunger = Mathf.Max(0f, Hunger - config.hungerDecay * Time.deltaTime);
        Thirst = Mathf.Max(0f, Thirst - config.thirstDecay * Time. deltaTime);
        Tiredness = Mathf.Max(0f, Tiredness - config.tiredDecay * Time.deltaTime);

        if (Hunger <= 0 || Thirst <= 0f)
            ApplyDamage(config.starveDPS * Time.deltaTime);
    }

    public void Eat(float amount) => Hunger = Mathf.Min(100f, Hunger + amount);
    public void Drink(float amount) => Thirst = Mathf.Min(100, Thirst + amount);
    public void Rest(float amount) => Tiredness = Mathf.Min(100, Tiredness + amount);

    public void ApplyDamage(float amount)
    {
        Health = Mathf.Max(0f, Health - amount);
        if (Health <= 0f)
            OnDeath?.Invoke();

    }
}
