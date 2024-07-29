using UnityEngine;
using UnityEngine.UI;

namespace movement
{
    /// <summary>
    /// Manages player movement and animation.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] public float moveSpeed; ///< The speed at which the player moves.
        private Animator animator;
        private SpriteRenderer sr;
        private Vector2 movementInput;

        /// <summary>
        /// Initializes references to the Rigidbody2D, Animator, and SpriteRenderer components.
        /// </summary>
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            sr = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Reads input and updates the movement direction and animation state.
        /// </summary>
        private void Update()
        {
            float xInput = Input.GetAxisRaw("Horizontal");
            float yInput = Input.GetAxisRaw("Vertical");
            movementInput = new Vector2(xInput, yInput).normalized;
            UpdateAnimation(xInput, yInput);
        }

        /// <summary>
        /// Applies movement based on the current movement input.
        /// </summary>
        private void FixedUpdate()
        {
            HandleMovement();
        }

        /// <summary>
        /// Moves the player based on input and speed.
        /// </summary>
        private void HandleMovement()
        {
            Vector2 currentPosition = rb.position;
            Vector2 newPosition = currentPosition + movementInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }

        /// <summary>
        /// Updates the player's animation based on movement input and direction.
        /// </summary>
        /// <param name="xInput">Horizontal movement input.</param>
        /// <param name="yInput">Vertical movement input.</param>
        private void UpdateAnimation(float xInput, float yInput)
        {
            bool isWalking = (xInput != 0 || yInput != 0);
            animator.SetBool("IsWalking", isWalking);

            // Flip sprite based on movement direction
            if (xInput > 0)
            {
                sr.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            }
            else if (xInput < 0)
            {
                sr.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }
        }

        /// <summary>
        /// Triggers the attack animation.
        /// </summary>
        void OnFire()
        {
            animator.SetTrigger("Attack");
        }
    }
}
