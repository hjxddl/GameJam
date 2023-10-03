
using UnityEngine;

public interface IKickable
{
    public void EnterRadius();
    public void ExitRadius();
    public void SetVelocity(Vector2 _velocity);
    public void AddForce(Vector2 _direction);
    public void SetDrag(float _drag);
}
