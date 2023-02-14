using System;
using System.Collections.Generic;
using Managers.GameManagers;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PlayerMovementController
{
    #region FIELDS

    private const float MIN_SPEED_TO_SET = 0;
    private const float MAX_SPEED_TO_SET = 100;

    [FormerlySerializedAs("maxSpeed")] [Header("Movement")] [SerializeField, Range(MIN_SPEED_TO_SET, MAX_SPEED_TO_SET)]
    private float _maxSpeed = 1;

    [FormerlySerializedAs("accelerationFactory")] [SerializeField] private float _accelerationFactory = 1;
    [FormerlySerializedAs("brakingFactory")] [SerializeField] private float _brakingFactory = 1;
    [FormerlySerializedAs("playerRigidBody2D")] [SerializeField] private Rigidbody2D _playerRigidBody2D = null;

    private MovingStateEnum _state = MovingStateEnum.IDLE;
    private List<Guid> _keysIds  = new List<Guid>();

    private IUpdateManager _updateManager;
    private IKeyboardManager _keyboardManager;
    private IGameMainManager _gameMainManager;
    private IInputManager _inputManager;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public void InjectDependencies(IUpdateManager updateManager, IKeyboardManager keyboardManager, IGameMainManager gameMainManager, IInputManager inputManager)
    {
        _updateManager = updateManager;
        _keyboardManager = keyboardManager;
        _gameMainManager = gameMainManager;
        _inputManager = inputManager;
    }

    public void Initialize()
    {
        InitializeKeys();
        InitializeUpdate();
    }

    public void ResetPosition()
    {
        _playerRigidBody2D.transform.position = Vector3.zero;
    }

    public void MoveUp()
    {
        _playerRigidBody2D.AddForce(Vector2.up * _accelerationFactory);
    }

    public void MoveDown()
    {
        _playerRigidBody2D.AddForce(Vector2.down * _accelerationFactory);
    }

    public void MoveRight()
    {
        _playerRigidBody2D.AddForce(Vector2.right * _accelerationFactory);
    }

    public void MoveLeft()
    {
        _playerRigidBody2D.AddForce(Vector2.left * _accelerationFactory);
    }

    public void MoveUpRight()
    {
        _playerRigidBody2D.AddForce(new Vector2(1, 1) * _accelerationFactory);
    }

    public void MoveUpLeft()
    {
        _playerRigidBody2D.AddForce(new Vector2(-1, 1) * _accelerationFactory);
    }

    public void MoveDownLeft()
    {
        _playerRigidBody2D.AddForce(new Vector2(-1, -1) * _accelerationFactory);
    }

    public void MoveDownRight()
    {
        _playerRigidBody2D.AddForce(new Vector2(1, -1) * _accelerationFactory);
    }

    public void Brake()
    {
        _playerRigidBody2D.AddForce(-_brakingFactory * _playerRigidBody2D.velocity);
    }

    public void LimitVelocity()
    {
        Vector3 normalizedVelocity = _playerRigidBody2D.velocity.normalized;
        Vector3 brakeVelocity = normalizedVelocity * _maxSpeed; // make the brake Vector3 value

        _playerRigidBody2D.velocity = brakeVelocity; // apply opposing brake force
    }

    private void HandleVelocityLimit()
    {
        if (_state != MovingStateEnum.BREAKING && _playerRigidBody2D.velocity.magnitude > _maxSpeed)
        {
            LimitVelocity();
        }
    }

    private void InitializeKeys()
    {
        _gameMainManager.OnGameStart += AttachKeysForMovement;
        _gameMainManager.OnWaitingOpen += DetachKeysForMovement;
        _gameMainManager.OnMainOpen += DetachKeysForMovement;
    }

    private void AttachKeysForMovement()
    {
        _keysIds.Add(_keyboardManager.AddKey(GetKeyCodeMoveUp(), SetMoveUp));
        _keysIds.Add(_keyboardManager.AddKey(GetKeyCodeMoveDown(), SetMoveDown));
        _keysIds.Add(_keyboardManager.AddKey(GetKeyCodeMoveLeft(), SetMoveLeft));
        _keysIds.Add(_keyboardManager.AddKey(GetKeyCodeMoveRight(), SetMoveRight));
        _keysIds.Add(_keyboardManager.AddKey(GetKeysCodeMoveUpRight(), SetMoveUpRight));
        _keysIds.Add(_keyboardManager.AddKey(GetKeysCodeMoveUpLeft(), SetMoveUpLeft));
        _keysIds.Add(_keyboardManager.AddKey(GetKeysCodeMoveDownRight(), SetMoveDownRight));
        _keysIds.Add(_keyboardManager.AddKey(GetKeysCodeMoveDownLeft(), SetMoveDownLeft));
        _keysIds.Add(_keyboardManager.AddKey(GetKeysCodeMovement(), SetBreak, KeyInput.KeyStateEnum.KEY_HOLD,
            KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum.KEY_HAS_NOT_OCCUR));
    }

    private void DetachKeysForMovement()
    {
        for (int i = _keysIds.Count - 1; i >= 0; i--)
        {
            _keyboardManager.RemoveKey(_keysIds[i]);
            _keysIds.RemoveAt(i);
        }
    }

    private void InitializeUpdate()
    {
        _updateManager.OnDataChange += HandleState;
    }

    private void SetMoveUp()
    {
        _state = MovingStateEnum.MOVING_UP;
    }

    private void SetMoveDown()
    {
        _state = MovingStateEnum.MOVING_DOWN;
    }

    private void SetMoveLeft()
    {
        _state = MovingStateEnum.MOVING_LEFT;
    }

    private void SetMoveRight()
    {
        _state = MovingStateEnum.MOVING_RIGHT;
    }

    private void SetMoveUpLeft()
    {
        _state = MovingStateEnum.MOVING_UP_LEFT;
    }

    private void SetMoveUpRight()
    {
        _state = MovingStateEnum.MOVING_UP_RIGHT;
    }

    private void SetMoveDownLeft()
    {
        _state = MovingStateEnum.MOVING_DOWN_LEFT;
    }

    private void SetMoveDownRight()
    {
        _state = MovingStateEnum.MOVING_DOWN_RIGHT;
    }

    private void SetBreak()
    {
        _state = MovingStateEnum.BREAKING;
    }

    private void HandleState()
    {
        switch (_state)
        {
            case MovingStateEnum.MOVING_DOWN:
                MoveDown();
                break;
            case MovingStateEnum.MOVING_UP:
                MoveUp();
                break;
            case MovingStateEnum.MOVING_RIGHT:
                MoveRight();
                break;
            case MovingStateEnum.MOVING_LEFT:
                MoveLeft();
                break;
            case MovingStateEnum.MOVING_UP_LEFT:
                MoveUpLeft();
                break;
            case MovingStateEnum.MOVING_UP_RIGHT:
                MoveUpRight();
                break;
            case MovingStateEnum.MOVING_DOWN_RIGHT:
                MoveDownRight();
                break;
            case MovingStateEnum.MOVING_DOWN_LEFT:
                MoveDownLeft();
                break;
            case MovingStateEnum.BREAKING:
                Brake();
                break;
        }

        HandleVelocityLimit();
    }

    private KeyCode GetKeyCodeMoveUp()
    {
        return _inputManager.KeyCodeMoveUp;
    }

    private KeyCode GetKeyCodeMoveDown()
    {
        return _inputManager.KeyCodeMoveDown;
    }

    private KeyCode GetKeyCodeMoveLeft()
    {
        return _inputManager.KeyCodeMoveLeft;
    }

    private KeyCode GetKeyCodeMoveRight()
    {
        return _inputManager.KeyCodeMoveRight;
    }

    private List<KeyCode> GetKeysCodeMoveUpRight()
    {
        return new List<KeyCode>() { GetKeyCodeMoveUp(), GetKeyCodeMoveRight() };
    }

    private List<KeyCode> GetKeysCodeMoveUpLeft()
    {
        return new List<KeyCode>() { GetKeyCodeMoveUp(), GetKeyCodeMoveLeft() };
    }

    private List<KeyCode> GetKeysCodeMoveDownRight()
    {
        return new List<KeyCode>() { GetKeyCodeMoveDown(), GetKeyCodeMoveRight() };
    }

    private List<KeyCode> GetKeysCodeMoveDownLeft()
    {
        return new List<KeyCode>() { GetKeyCodeMoveDown(), GetKeyCodeMoveLeft() };
    }

    private List<KeyCode> GetKeysCodeMovement()
    {
        return new List<KeyCode>()
            { GetKeyCodeMoveDown(), GetKeyCodeMoveUp(), GetKeyCodeMoveLeft(), GetKeyCodeMoveRight() };
    }

    #endregion

    #region ENUMS

    public enum MovingStateEnum
    {
        IDLE,
        BREAKING,
        MOVING_UP,
        MOVING_UP_LEFT,
        MOVING_UP_RIGHT,
        MOVING_LEFT,
        MOVING_RIGHT,
        MOVING_DOWN,
        MOVING_DOWN_LEFT,
        MOVING_DOWN_RIGHT,
    }

    #endregion
}