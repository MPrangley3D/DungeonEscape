using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public GameObject _playerSprite;
    public GameObject _trailSprite;
    private Animator playerAnim;
    private Animator trailAnim;

    public bool hasKey { get; set; }
    public bool hasSword { get; set; }
    public bool hasBoots { get; set; }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is Null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        playerAnim = _playerSprite.GetComponent<Animator>();
        trailAnim = _trailSprite.GetComponent<Animator>();
    }

    public void upgradeSword()
    {
        playerAnim.SetBool("FlameSword", true);
        trailAnim.SetBool("FlameSword", true);
    }

}