using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public float rotationSpeed = 270f;
    public float moveSpeed = 5f;
    void Update()
    {
        characterController.SimpleMove(transform.forward * Input.GetAxis("Vertical") * moveSpeed);
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
    }
}
