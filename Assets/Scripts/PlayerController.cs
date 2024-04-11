using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState 
{ 
    Move,
    Attack,
    Interact
}

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public Animator animator;

    private InputManager inputManager;
    private PlayerInput playerInput;
    private InputAction movementAction;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    private PlayerState playerState;

    // first things before Start
    void Awake()
    {
        inputManager = InputManager.Instance;
        playerInput = GetComponent<PlayerInput>();//jake chceme komponentu a delat s ni

        movementAction = playerInput.actions["Movement"];

        rb = GetComponent<Rigidbody2D>();

       movementAction.performed += Move; //nemuse se tqk esit podminky

        playerState = PlayerState.Move;
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        //Debug.Log(ctx.phase);
        if (!ctx.performed)
        {
            return;
        }
    }

    public void Attack(InputAction.CallbackContext ctx)
    { 
        if (!ctx.performed) return;

        StartCoroutine(AttackCo());

    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        playerState = PlayerState.Attack;
        yield return null;
        animator.SetBool("Attacking", false);
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackDown"))
        { 
            yield return null;
        }
        playerState = PlayerState.Move;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //60x sekunda akce hrace vypis fps
    {
        moveDirection = Vector2.zero;

        if (playerState == PlayerState.Move)
        {
            moveDirection = movementAction.ReadValue<Vector2>();

            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
            animator.SetFloat("Speed", moveDirection.sqrMagnitude); 
        }
    }

    void FixedUpdate()//50x za sekundu fyzikalni akce prvni se vola 
    {
        rb.MovePosition(rb.position + moveDirection * (playerSpeed * Time.fixedDeltaTime));
    }

    private void LateUpdate() // 60x za sekundu pohyb kamerou
    {
        
    }
}
