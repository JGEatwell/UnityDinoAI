public interface IDinoState
{
    void Enter(DinoController controller);
    void Exit(DinoController controller);

    IDinoState Tick(DinoController controller);
}