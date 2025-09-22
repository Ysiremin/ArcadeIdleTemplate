using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    
    private FloatingJoystick joystick;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        joystick = UIManager.instance.joystick;
    }

    private void Update()
    {
        var horizontal = joystick.Horizontal;
        var vertical = joystick.Vertical;

        var direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1f)
        {
            direction.Normalize();

            controller.Move(direction * (moveSpeed * Time.deltaTime));

            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}