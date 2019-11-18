using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float BaseSpeed = 6f;
    public int ControllerID;

    [SerializeField] private Animator _animator;
    private Rigidbody2D _rb2d;
    private Player _player;
    private int _playerNumber;
    private Vector2 _movement;
    private float _faceY;
    private float _faceX;
    private float _speed;
    private bool _pressedAttack;
    private float _attackTime;
    private float _attackAnimCounter;
    private bool _attacking;
    private int _gateID;
    private bool _enteringGate = true;
    private Dictionary<int, Vector2> _gateIds;
    private List<float> _speedModifiers = new List<float>();
    private CircleCollider2D[] _swordColliders;
    float _mainInputHorizontal;
    float _mainInputVertical;

    private Vector2 _lookDirection;
    private float _lookV;
    private List<Enemy> _enemiesHit = new List<Enemy>();

    private void Start()
    {
        _player = GetComponent<Player>();
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        // TODO: This is temporary. Values should be populated automatically by getting gates from map so game positions can be moved without the need to change code. 
        _gateIds = new Dictionary<int, Vector2>() { { -1, new Vector2(72f, -7f) }, { 1, new Vector2(-10.5f, 3.5f) }, { 2, new Vector2(-11.5f, -7.5f) } };
        _playerNumber = _player.PlayerNumber;
        _speed = BaseSpeed;

        _swordColliders = transform.Find("MeleeColliders").GetComponentsInChildren<CircleCollider2D>();

        foreach (var collider in _swordColliders)
        {
            collider.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

        SetAnimatorLayer(_player.EquipmentID);
    }

    private void SetAnimatorLayer(int index = -1, string name = "")
    {
        if (index >= 0 && index < _animator.layerCount)
        {
            for (int i = 0; i < _animator.layerCount; i++)
            {
                if (i != index)
                    _animator.SetLayerWeight(i, 0);
            }
            _animator.SetLayerWeight(index, 1);
            SetAttackTime(index);
        }
    }

    private void SetAttackTime(int layerID)
    {
        string animName = "";
        switch (layerID)
        {
            case 0:
                animName = "AttackDownBase";
                break;
            case 1:
                animName = "AttackDownDagger";
                break;
        }

        if (animName.Equals("")) return;

        RuntimeAnimatorController ac = _animator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == animName)
            {
                _attackTime = ac.animationClips[i].length / 3;
                break;
            }
        }
    }

    private void Update()
    {
        _mainInputHorizontal = InputManager.MainHorizontal(ControllerID);
        _mainInputVertical = InputManager.MainVertical(ControllerID);

        _lookDirection = new Vector2(Input.GetAxisRaw("HorizontalLook_1"), Input.GetAxisRaw("VerticalLook_1"));


        for (int i = 0; i < _enemiesHit.Count; i++)
        {
            _enemiesHit[0].Damage(5);
            _enemiesHit.RemoveAt(0);
        }
    }

    private void FixedUpdate()
    {
        // Store the input axes.

        Move(_mainInputHorizontal, _mainInputVertical);

        _pressedAttack = PerformAttack();

        Animating(_mainInputHorizontal, _mainInputVertical, _pressedAttack);
    }

    void Move(float h, float v)
    {
        _movement.Set(h, v);
        _movement = Vector2.ClampMagnitude(_movement, 1f);

        _speed = BaseSpeed * (Mathf.Abs(_movement.x) + Mathf.Abs(_movement.y));
        _speedModifiers.ForEach(n => _speed *= n);

        _movement = _movement * _speed * Time.deltaTime;
        _rb2d.MovePosition(new Vector2(transform.position.x, transform.position.y) + _movement);
    }


    void Animating(float h, float v, bool attackPressed)
    {
        if (_animator.GetLayerWeight(_player.EquipmentID) != 1)
            SetAnimatorLayer(_player.EquipmentID);

        bool walking = h != 0f || v != 0f;

        float speed = (Mathf.Abs(_movement.x) + Mathf.Abs(_movement.y)) / 2;

        if (_lookDirection != Vector2.zero)
        {
            _animator.SetFloat("Horizontal", _lookDirection.x);
            _animator.SetFloat("Vertical", _lookDirection.y);
        }


        _animator.SetFloat("Speed", speed);
        _animator.SetInteger("EquipmentID", _player.EquipmentID);

        Vector2 walkAnimSpeed = new Vector2(h, v).normalized;

        _animator.SetFloat("WalkAnimSpeed", (Mathf.Abs(h) + Mathf.Abs(v)) / 2);

        if (attackPressed && !_attacking)
        {
            _pressedAttack = false;
            _speedModifiers.Add(.3f);
            _animator.SetFloat("Attacking 0", 1);
            _attackAnimCounter = _attackTime;
            _attacking = true;
        }
        else if (_attackAnimCounter < 0 && _attacking)
        {
            _speedModifiers.Remove(.3f);
            _animator.SetFloat("Attacking 0", 0);
            _attacking = false;
        }
        else
        {
            if (_attackAnimCounter >= 0)
                _attackAnimCounter -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            GameObject interactedObject = collision.gameObject;
            Interactable interactable = interactedObject.GetComponent<Interactable>();
            interactable.Interact(_player);
        }

        if (collision.CompareTag("Enemy"))
        {
            _enemiesHit.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }

    public void UseGate(string sceneName, int gateID, bool portingToGate)
    {
        if (_enteringGate)
        {
            transform.position = _gateIds[gateID];
            _enteringGate = !portingToGate;

            Scene targetScene = SceneManager.GetSceneByName(sceneName);
            if (targetScene != null)
                SceneManager.LoadScene(sceneName);

        }
        else
        {
            _enteringGate = true;
        }
    }

    private bool PerformAttack()
    {
        if (_attacking) return false;
        return (Input.GetAxisRaw("TriggerFire1_1") > .0001f);
    }

    public void MoveToNextScreen(Vector2 movement)
    {
        transform.position = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, transform.position.z);
    }


}
