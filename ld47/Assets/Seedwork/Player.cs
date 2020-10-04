using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public float rotationSpeed = 270f;
    public float moveSpeed = 5f;
    public float maxInteractionDistance = 1f;
    public Vector3 StartPosition;
    public Quaternion StartRotation;
    private bool reset = false;

    void Start()
    {
        StartPosition = transform.position;
        StartRotation = transform.rotation;
    }
    void Update()
    {
        if (reset)
        {
            characterController.enabled = false;
            transform.SetPositionAndRotation(StartPosition, StartRotation);
            characterController.enabled = true;
            reset = false;
        }
        if (!GameConductor.IsPlayerFrozen)
        {
            // handle input
            characterController.SimpleMove(transform.forward * Input.GetAxis("Vertical") * moveSpeed);
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var raycast = Physics.RaycastAll(transform.position + Vector3.up * 1.5f, transform.forward, maxInteractionDistance, LayerMask.GetMask("Interactable"));
                if (raycast.Any())
                {
                    raycast.First().collider.GetComponent<Interactable>()?.OnInteract();
                    // todo: play sound
                }
                else
                {
                    // todo: play sound
                }
            }
        }
    }
    public static void Reset()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().reset = true;
    }
}
