using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement pars")]
    [SerializeField]private float speed;
    [SerializeField]private float jumpForce;
    private float _horizontalInput;
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask enemyLayer;
    [Header("Monos")]
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private Health _health;
    [Header("Cooldown")]
    private float _wallJumpCooldown;
    private float _dodgeCooldown;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip playerJumpSound;
    [SerializeField] private AudioClip playerDodgeSound;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _health = GetComponent<Health>();
    }
    
    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        CheckDirection(_horizontalInput);
        
        //set anim paramets
        _animator.SetBool("isRunning", _horizontalInput != 0);
        _animator.SetBool("isGrounded", isGrounded());

        WallJump(_horizontalInput);
        Dodge();

    }

    private void Dodge()
    {
        if (_dodgeCooldown > 1)
        {
            if (!isOnWall() && Input.GetMouseButtonDown(1))
            {
                _animator.SetTrigger("Dodge");
                SoundManager.instance.PlaySound(playerDodgeSound);
                StartCoroutine(_health.Invunerability());
                _dodgeCooldown = 0;
            }
        }
        else
            _dodgeCooldown += Time.deltaTime;
    }

    private void WallJump(float horizontalInput)
    {
        //wall jump logic
        if (_wallJumpCooldown > 0.2f)
        {
            _rigidbody2D.velocity = new Vector2(horizontalInput * speed, _rigidbody2D.velocity.y);
            if (isOnWall() && !isGrounded())
            {
                _rigidbody2D.gravityScale = 0;
                _rigidbody2D.velocity = Vector2.zero;
            }
            else
                _rigidbody2D.gravityScale = 2.5f;

            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            {
                Jump();
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
                  SoundManager.instance.PlaySound(playerJumpSound);
            }
            
        }
        else
            _wallJumpCooldown += Time.deltaTime;
        
    }
    private void Jump()
        {
            if (isGrounded() || isOnWallBox() || isOnEnemy())
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
                _animator.SetTrigger("Jump");
            }
            else if (isOnWall() && !isGrounded())
            {
                if (_horizontalInput == 0)
                {
                    _rigidbody2D.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y,
                        transform.localScale.z);
                }
                else
                    _rigidbody2D.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                _wallJumpCooldown = 0;
            }
        }
    
    private void CheckDirection(float horizontalInput)
    {
        transform.localScale = horizontalInput switch
        {
            //flip player left/right
            > 0.01f => Vector3.one,
            < -0.01f => new Vector3(-1, 1, 1),
            _ => transform.localScale
        };
    }
    

    public bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size,
            0, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }

    public bool isOnWallBox()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size,
            0, Vector2.down, 0.1f, wallLayer);
        return raycastHit2D.collider != null;
    }

    public bool isOnWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size,
            0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit2D.collider != null;
    }
    
    public bool isOnEnemy()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size,
            0, Vector2.down, 0.1f, enemyLayer);
        return raycastHit2D.collider != null;
    }
    
}
