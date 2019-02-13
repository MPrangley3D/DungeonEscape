using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpforce = 3.0f;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private bool _jumpCooldown = false;
    [SerializeField]
    private float _playerSpeed = 3.0f;
    [SerializeField]
    private float _playerJumpHeight = 1.2f;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _effectsSprite;
    private bool _grounded = false;
    private PlayerAnimation _playerAnim;
    private Animator _anim;
    private bool dead = false;

    public int Health { get; set; }
    
    void Start()
    {
        Health = 4;
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _anim = GetComponentInChildren<Animator>();
        _playerSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _effectsSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (dead == false)
        { 
            Movement();
            _grounded = IsGrounded();

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && _grounded == true)
            {
                if(GameManager.Instance.hasSword == true)
                {
                    _playerAnim.Attack();
                }
                else
                {
                    _playerAnim.Attack();
                }
            }
#else
            if (CrossPlatformInputManager.GetButtonDown("A_Btn") && _grounded == true)
            {
                _playerAnim.Attack();
            }
#endif
        }
    }

    void Movement()
    {
        //Walk
#if UNITY_STANDALONE || UNITY_WEBPLAYER|| UNITY_EDITOR
        float move = Input.GetAxisRaw("Horizontal");
#else
        float move = CrossPlatformInputManager.GetAxisRaw("Horizontal");
#endif

        Flip(move);

        //Jump
#if UNITY_STANDALONE || UNITY_WEBPLAYER|| UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
#else
        if (CrossPlatformInputManager.GetButtonDown("B_Btn") && _grounded == true)
#endif
        {
            Debug.Log("jump");
            if (GameManager.Instance.hasBoots == true)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpforce+3 * _playerJumpHeight+2);
                StartCoroutine(JumpCooldown());
                _playerAnim.Jump(true);
            }
            else
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpforce * _playerJumpHeight);
                StartCoroutine(JumpCooldown());
                _playerAnim.Jump(true);
            }

        }

        _rigid.velocity = new Vector2(move * _playerSpeed, _rigid.velocity.y);
        _playerAnim.Move(move);
    }

    void Flip(float move)
    {
        Vector3 newLocalScale = transform.localScale;
        var size = Mathf.Abs(transform.localScale.x);

        if (move > 0)
        {
            newLocalScale.x = size;
        }

        else if (move < 0)
        {
            newLocalScale.x = -size;
        }

        transform.localScale = newLocalScale;
    }

    bool IsGrounded()
    {
        //Ground Collision
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, _groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down * 0.9f, Color.red);
        if (hitInfo.collider != null && _jumpCooldown == false)
        {
            _playerAnim.Jump(false);
            return true;
        }
        return false;
    }

    IEnumerator JumpCooldown()
    {
        //Jump Cooldown
        _jumpCooldown = true;
        yield return new WaitForSeconds(0.1f);
        _jumpCooldown = false;
    }

    public void Damage()
    {
        if (dead == false)
        {
            Health--;
            Debug.Log("Current HP: " + Health);
            UIManager.Instance.UpdateLife(Health);
            if (Health < 0)
            {
                dead = true;
                _rigid.velocity = new Vector2(0, 0);
                _anim.SetTrigger("Death");
                StartCoroutine(Restart());
            }
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Game");
    }
}