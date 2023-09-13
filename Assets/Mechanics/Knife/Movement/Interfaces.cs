public interface IRotable
{
    void HandleRotation();
    void CanRotate();
    bool _jump { get; set; }
}
public interface IMoveable
{
    void HandleMovement();
}
public interface IJumpable
{
    void HandleJump();

}