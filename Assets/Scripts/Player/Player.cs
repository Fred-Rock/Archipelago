using UnityEngine;

public class Player : SingletonMonobehaviour<Player>
{
    private float xInput;
    private float yInput;

    private bool isIdle;
    private bool isWalking;

    private Camera mainCamera;

    private Rigidbody2D rb2d;

    private Direction direction;

    private float movementSpeed;

    private bool _playerInputIsDisabled = false;

    private int currency = 100;

    public bool PlayerInputIsDisabled { get => _playerInputIsDisabled; set => _playerInputIsDisabled = value; }

    public Direction PlayerFacingDirection { get => direction; set => direction = value; }

    public int Currency { get => currency; set => currency = value; }

    protected override void Awake()
    {
        base.Awake();

        rb2d = GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;
    }

    private void Update()
    {
        #region Player Input

        if (!PlayerInputIsDisabled)
        {
            ResetAnimationTriggers();

            PlayerMovementInput();

            PlayerTestInput();

            EventHandler.CallMovementEvent(xInput, yInput, isWalking, isIdle);
        }

        #endregion
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector2 move = new Vector2(xInput * movementSpeed * Time.deltaTime, yInput * movementSpeed * Time.deltaTime);

        rb2d.MovePosition(rb2d.position + move);
    }

    private void PlayerMovementInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (xInput != 0 && yInput != 0)
        {
            xInput = xInput * 0.71f;
            yInput = yInput * 0.71f;
        }

        if (xInput != 0 || yInput != 0)
        {
            isIdle = false;
            isWalking = true;
            movementSpeed = Settings.walkingSpeed;

            // Capture player direction for save game.
            if (xInput < 0)
            {
                direction = Direction.left;
            }
            else if (xInput > 0)
            {
                direction = Direction.right;
            }
        }
        else if (xInput == 0 && yInput == 0)
        {
            isWalking = false;
            isIdle = true;
        }
    }

    public void DisablePlayerInputAndResetMovement()
    {
        DisablePlayerInput();
        ResetMovement();

        EventHandler.CallMovementEvent(xInput, yInput, isWalking, isIdle);
    }

    private void ResetMovement()
    {
        xInput = 0f;
        yInput = 0f;
        isWalking = false;
        isIdle = false;
    }

    public void EnablePlayerInput()
    {
        PlayerInputIsDisabled = false;
    }

    public void DisablePlayerInput()
    {
        PlayerInputIsDisabled = true;
    }

    private void ResetAnimationTriggers()
    {
        // Used to set animation triggers to false, e.g. isUsingTool = false;
    }

    public Vector3 GetPlayerViewportPosition()
    {
        // Viewport position for the player is (0, 0) at bottom-left, and (1, 1) at top-right
        return mainCamera.WorldToViewportPoint(transform.position);
    }

    private void PlayerTestInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneControllerManager.Instance.FadeAndLoadScene(SceneName.Scene0_Camp.ToString(), transform.position);
        }
    }
}