using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] InputAction horizontalInput;
    public float moveSpeedMultiplier;
    public Transform camTransform;
    
    public float difficultyMultiplier = 0.1f;
    public float screenOffset;

    Transform _transform;
    public Rigidbody2D rb;
    
    Vector3 _camPos;
    float _camVelocity;
    Vector2 _moveAccel;
    

    bool OnScreen => rb.position.y > camTransform.position.y - screenOffset;

    void Awake()
    {
        _transform = transform;
        _camPos = camTransform.position;
        _camPos.z = -10;
        horizontalInput.Enable();
    }

    void OnEnable()
    {
        _moveAccel = Vector2.zero;
        horizontalInput.performed += HorizontalInputOnperformed;
    }

    void OnDisable()
    {
        horizontalInput.performed -= HorizontalInputOnperformed;
    }
    
    void HorizontalInputOnperformed(InputAction.CallbackContext obj)
    {
        _moveAccel.x = obj.ReadValue<float>() * moveSpeedMultiplier;
        _moveAccel.y = Mathf.Abs(obj.ReadValue<float>()) * moveSpeedMultiplier;
    }


    void Update()
    {
        _camPos.y = Mathf.Max(_transform.position.y, camTransform.position.y);
        _camVelocity = difficultyMultiplier * _camPos.y;
        _camPos.y += _camVelocity * Time.deltaTime;
        camTransform.position = _camPos;

        if (OnScreen) return;
        
        GameLoop.Inst.InvokeDeath();
    }

    void FixedUpdate()
    {
        rb.velocity += _moveAccel * Time.fixedDeltaTime;
    }

    public void Spawn()
    {
        _camVelocity = 1f;
        _camPos.y = 0;
        camTransform.position = _camPos;
        rb.velocity = Vector2.zero;
        rb.transform.position = new Vector3(_camPos.x, _camPos.y);
    }
}
