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

    #region ������� PlayerInput (���� �� ��������)
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

    // ����� ��� �������� ��������� ��� �����������
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

    // ����� ��� ����������� ���������
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

    // ����� ��� ������ ���������
    private void JumpMove()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_animator.GetBool(_animatorBools[3]))
        {
            _animator.SetBool(_animatorBools[3], true);
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    // ����� ��� ����� ���������
    private void AttackMove()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsStandingStill() && _canAttack)
        {
            _animator.SetBool(_animatorBools[1], true);
        }
    }

    // ��������, ��������� �� �������� � ��������� "������"
    private bool IsHit()
    {
        return _animator.GetBool(_animatorBools[2]);
    }

    // ��������, ����� �� �������� ��������
    private bool IsStandingStill()
    {
        return !_animator.GetBool(_animatorBools[0]) && !_animator.GetBool(_animatorBools[3]);
    }

    // public ������ ��� ���� ����� ������������ ������� ��������
    public void GetHit()
    {
        _animator.SetBool(_animatorBools[2], true);
    }

    public void ChangeInputAccess()
    {
        _canInput = !_canInput;
    }

    // ������ � ���������
    private void EndOfAttack()
    {
        _animator.SetBool(_animatorBools[1], false);
    }

    private void RecoverFromHit()
    {
        _animator.SetBool(_animatorBools[2], false);
    }
}
