using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private Animator animator;
        
        private Rigidbody2D rigibody;
        
        private CircleCollider2D groundCircle;
        private LayerMask groundLayers;
        
        // Start is called before the first frame update
        void Start()
        {
            rigibody = GetComponent<Rigidbody2D>();
            groundCircle = GetComponentInChildren<CircleCollider2D>();

            groundLayers = LayerMask.GetMask("Ground");
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            Jump();

            SetAnimation();
        }

        private void SetAnimation()
        {
            animator.SetFloat("movementSpeed", rigibody.velocity.x);
        }

        private void Move()
        {
            float xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
            rigibody.velocity = new Vector2(xMovement, rigibody.velocity.y);
        }
        
        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            {
                rigibody.velocity = new Vector2(rigibody.velocity.x, jumpForce);
            }
        }

        private bool isGrounded()
        {
            Collider2D collider = Physics2D.OverlapCircle(groundCircle.transform.position, groundCircle.radius, groundLayers);

            return collider != null;
        }
    }
    
}
