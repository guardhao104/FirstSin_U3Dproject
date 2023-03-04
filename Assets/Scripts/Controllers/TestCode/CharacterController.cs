using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharacterController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset walking;
    public string currentState;
    public float speed;
    public float movement;
    private Rigidbody2D r2b;
    public string currentAnimation;

    // Start is called before the first frame update
    void Start()
    {
        r2b = GetComponent<Rigidbody2D>();
        currentState = "Idle";
        SetCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        // if the playing animation equal to current animation, stop load animation again
        if(animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    public void SetCharacterState(string state)
    {
        if(state.Equals("Idle"))
        {
            SetAnimation(idle, true, 1f);
        }
        else if(state.Equals("Walking"))
        {
            SetAnimation(walking, true, 1f);
        }
    }

    public void Move()
    {
        movement = Input.GetAxis("Horizontal");
        r2b.velocity = new Vector2(movement * speed, r2b.velocity.y);

        if(movement != 0)
        {
            SetCharacterState("Walking");
            if(movement > 0)
            {
                transform.localScale = new Vector2(0.5f, 0.5f);
            }
            else
            {
                transform.localScale = new Vector2(-0.5f, 0.5f);
            }
        }
        else
        {
            SetCharacterState("Idle");
        }
    }
}
