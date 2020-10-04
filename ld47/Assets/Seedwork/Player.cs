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
    private Interactable selected;

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

            SelectInteractable();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var interactable = ChooseInteractable();
                if (interactable != null) {
                    interactable.OnInteract();
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
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.reset = true;
        DeselectInteractable();
    }
    private Interactable ChooseInteractable()
    {
        var raycast = Physics.RaycastAll(transform.position + Vector3.up * 1.5f, transform.forward, maxInteractionDistance, LayerMask.GetMask("Interactable"));
        if (raycast.Any())
        {
            RaycastHit hit = raycast.OrderBy(h => h.distance).First();
            return hit.collider.GetComponent<Interactable>();
        }
        return null;
    }
    private void SelectInteractable()
    {
        var newSelected = ChooseInteractable();
        if (newSelected != selected)
        {
            DeselectInteractable();
            selected = newSelected;
            selected?.OnSelect();
        }
    }
    public static void DeselectInteractable()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.selected?.OnDeselect();
        player.selected = null;
    }
}
