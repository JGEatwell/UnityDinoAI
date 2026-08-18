using UnityEngine;

public class DinoStateMachine
{
    private readonly DinoController controller;
    public IDinoState CurrentState { get; private set; }

    public DinoStateMachine(DinoController controller, IDinoState initialState)
    {
        this.controller = controller;
        CurrentState = initialState;
        CurrentState.Enter(controller);
    }

    public void Tick()
    {
        IDinoState nextState = CurrentState.Tick(controller);

        if (nextState != null & nextState != CurrentState)
        {
            CurrentState.Exit(controller);
            CurrentState = nextState;
            CurrentState.Enter(controller);
        }
    }
}
