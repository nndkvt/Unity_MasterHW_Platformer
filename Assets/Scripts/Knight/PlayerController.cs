using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerAtributes))]
public class PlayerController : MonoBehaviour
{
    [Header("Move Variables")]
    [SerializeField, Range(0, 10)] private float _jumpForce = 5f;
    [SerializeField, Range(0, 10)] private float _runSpeed = 3f;
    [SerializeField] private bool _canAttack;

    [Header("Animator Move Variables")]
    [SerializeField] private string[] _animatorBools = { "IsRunning", "IsAttacking", "IsHit", "IsInAir" };

    [Header("Player Controls")]
    [SerializeField] private InputAction _playerInput;

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private PlayerAtributes _atributes;
    private bool _canInput = true;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _atributes = GetComponent<PlayerAtributes>();
    }

    #region Будущий PlayerInput (пока не работает)
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
    #endregion

    private void Update()
    {
        if (_atributes.health > 0 && _canInput)
        {
            JumpMove();
            AttackMove();
        }
    }

    private void FixedUpdate()
    {
        if (_atributes.health > 0 && _canInput)
        {
            HorizontalMove();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Platform"))
        {
            _animator.SetBool(_animatorBools[3], false);
        }
    }

    // Метод для поворота персонажа при перемещении
    private void SetFlip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    // Метод для перемещения персонажа
    private void HorizontalMove()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && !IsHit())
        {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal") * _runSpeed, _rigidBody.velocity.y);

            _animator.SetBool(_animatorBools[0], true);
            _rigidBody.velocity = direction;

            SetFlip();
        }
        else
        {
            _animator.SetBool(_animatorBools[0], false);
        }
    }

    // Метод для прыжка персонажа
    private void JumpMove()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_animator.GetBool(_animatorBools[3]))
        {
            _animator.SetBool(_animatorBools[3], true);
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    // Метод для атаки персонажа
    private void AttackMove()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsStandingStill() && _canAttack)
        {
            _animator.SetBool(_animatorBools[1], true);
        }
    }

    // Проверка, находится ли персонаж в положении "ударен"
    private bool IsHit()
    {
        return _animator.GetBool(_animatorBools[2]);
    }

    // Проверка, стоит ли персонаж спокойно
    private bool IsStandingStill()
    {
        return !_animator.GetBool(_animatorBools[0]) && !_animator.GetBool(_animatorBools[3]);
    }

    // public потому что этот метод используется другими классами
    public void GetHit()
    {
        _animator.SetBool(_animatorBools[2], true);
    }

    public void ChangeInputAccess()
    {
        _canInput = !_canInput;
    }

    // Методы в аниматоре
    private void EndOfAttack()
    {
        _animator.SetBool(_animatorBools[1], false);
    }

    private void RecoverFromHit()
    {
        _animator.SetBool(_animatorBools[2], false);
    }
}
