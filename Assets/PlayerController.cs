using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpactionReference;
    [SerializeField] private InputActionReference flightActionReference1;
    [SerializeField] private InputActionReference flightActionReference2;
    [SerializeField] private InputActionProperty directionProperty1;
    [SerializeField] private InputActionProperty directionProperty2;
    [SerializeField] private double health = 100;
    [SerializeField] private double energy  = 100;

    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float flightForce = 5f;
    [SerializeField] private float drainPerSecond  = 5f;
    [SerializeField] private GameObject healthS;
    [SerializeField] private GameObject energyS;

    private Slider healthSlider;
    private Slider energySlider;

    private Rigidbody _body;
    private bool IsGrounded => Physics.Raycast(
        new Vector2(transform.position.x, transform.position.y + 2.0f),
        Vector3.down, 2.0f);
    private XROrigin _xrRig;
    private CapsuleCollider _collider;

    private Vector3 controller1Dir;
    private Vector3 controller2Dir;
    private float startFlying;
    private bool flyingLastFrame;
    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _xrRig = GetComponent<XROrigin>();
        jumpactionReference.action.performed += OnJump;
        startFlying = Time.time;
        healthSlider = healthS.GetComponent<Slider>();
        energySlider = energyS.GetComponent<Slider>();
        healthSlider.value = (float)health;
        energySlider.value = (float)energy;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var center = _xrRig.CameraInOriginSpacePos;
        _collider.height = Mathf.Clamp(_xrRig.CameraInOriginSpaceHeight, 1.0f, 3.0f);
        _collider.center = new Vector3(center.x, _collider.height / 2, center.z);

        if (flyingLastFrame)
        {
            if (Time.time - startFlying > .5)
            {
                if (!drainStamina(drainPerSecond/2.0))
                {
                    drainHP(drainPerSecond / 4.0);
                }
                startFlying = Time.time;
            }
        }
        if (flightActionReference1.action.IsPressed() & flightActionReference2.action.IsPressed()  && (health  >  0 &&  energy > 0))
        {
            print("FLY");
            controller1Dir = directionProperty1.action.ReadValue<Quaternion>()* Vector3.forward;
            controller2Dir = directionProperty2.action.ReadValue<Quaternion>() * Vector3.forward;
            Vector3 movementDir = controller1Dir + controller2Dir;
            movementDir = Vector3.Normalize(movementDir * flightForce);
            _body.velocity = movementDir;
            startFlying = Time.time;
            flyingLastFrame = true;
        }
        else
        {
            flyingLastFrame = false;
            startFlying = -1;
        }
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        _body.WakeUp();
        if (!IsGrounded) return;
        print("JUMP");
        _body.AddForce(Vector3.up * jumpForce);
    }

    public  void drainHP(double amt)
    {
        health -= amt;
        if (health < 0) health = 0;
        healthSlider.value = (float)health;
    }

    public bool drainStamina(double amt)
    {
        if(energy - amt > 0)
        {
            energy -= amt;
            energySlider.value = (float)energy;
            return true;
        }
        energySlider.value = (float)energy;
        return false;
    }

}
