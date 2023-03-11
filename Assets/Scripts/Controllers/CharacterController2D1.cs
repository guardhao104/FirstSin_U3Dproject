using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D1 : MonoBehaviour
{
    // Move player in 2D space
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpHeight = 0.0f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;

    // camera border
    public Transform farLeft;  // End of screen Left
    public Transform farRight;  //End of Screen Right

    // Controle player animation
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset walking;
    public AnimationReferenceAsset running;

    // animation state
    private string currentState;
    private string currentAnimation;

    private float cameraLeftBorder;
    private float cameraRightBorder;

    float speed = 0f;
    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        // Init (Joshua)
        currentState = "Idle";
        SetCharacterState(currentState);

        if (farLeft)
        {
            cameraLeftBorder = farLeft.position.x;
        }
        else
        {
            cameraLeftBorder = -Mathf.Infinity;
        }

        if (farRight)
        {
            cameraRightBorder = farRight.position.x;
        }
        else
        {
            cameraRightBorder = Mathf.Infinity;
        }

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        // Run/walk switch
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else if(moveDirection != 0) // When player press key, set speed (Joshua)
        {
            speed = walkSpeed;
        }
        else // No input, set speed to 0 (Joshua)
        {
            speed = 0;
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        // Camera follow
        if (mainCamera)
        {
            if (t.position.x > cameraRightBorder)
            {
                mainCamera.transform.position = new Vector3(cameraRightBorder, cameraPos.y, cameraPos.z);
            }
            else if (t.position.x < cameraLeftBorder)
            {
                mainCamera.transform.position = new Vector3(cameraLeftBorder, cameraPos.y, cameraPos.z);
            }
            else
            {
                mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
            }
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        Move(); // (Joshua)

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

    // Set animation (Joshua)
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        // if the playing animation equal to current animation, stop load animation again
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    // Set character state (Joshua)
    public void SetCharacterState(string state)
    {
        switch(state)
        {
            case "Idle":
                SetAnimation(idle, true, 1f);
                break;
            case "Walking":
                SetAnimation(walking, true, 1f);
                break;
            case "Running":
                SetAnimation(running, true, 1f);
                break;
            default:
                Debug.Log("Unknow state");
                break;
        }
    }

    // Move player and trigger animation (Joshua)
    private void Move()
    {
        var v = new Vector2((moveDirection) * speed, r2d.velocity.y);
        r2d.velocity = v;

        if (Mathf.Abs(v.x) == runSpeed)
        {
            SetCharacterState("Running");
        }
        else if (Mathf.Abs(v.x) == walkSpeed)
        {
            SetCharacterState("Walking");
        }
        else
        {
            SetCharacterState("Idle");
            speed = 0;
        }
    }


}