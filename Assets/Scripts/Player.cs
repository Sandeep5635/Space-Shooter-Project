using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    private float _thrusterSpeed = 7.0f;

    private Vector3 _spawnPosition = new Vector3(0 , .8f , 0);

    [SerializeField]
    private Vector3 _spawnTripleShotPos = new Vector3(0, 0, 0);

    [SerializeField]
    private float _fireRate = .5f;

    private float _canFire = -1f;

    [SerializeField]
    private GameObject laser_Prefab;

    [SerializeField]
    private GameObject tripleShot_prefab;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private int _score = 70;

    [SerializeField]
    private bool _tripleShotActive = false;

    [SerializeField]
    private bool _isshieldActive = false;

    private bool _isspeedUpActive = false;

    private SpawnManager _spawnManager;

    [SerializeField]
    private GameObject shields;

    private UIManager _UIManager;

    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;


    [SerializeField]
    private AudioClip _laserClip;

    [SerializeField]
    private AudioClip _explosionClip;

    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_UIManager == null)
        {
            Debug.LogError("UIManager is NULL");
        }

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        _audioSource = GetComponent<AudioSource>();

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource is NULL");
        }else
        {
            _audioSource.clip = _laserClip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire )
        {
            FireLaser();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            SpeedThruster();
   
        }else if(_isspeedUpActive == false)
        {
              _speed = 3.5f;
        }

    
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //   transform.Translate(Vector3.right *horizontalInput*_speed * Time.deltaTime);
        //   transform.Translate(Vector3.up * verticalInput * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        
        // if speed boost is collected then speed = 8.5

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);

        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
       

        if(_tripleShotActive == true)
        {
            Instantiate(tripleShot_prefab, transform.position + _spawnTripleShotPos,Quaternion.identity);
        }
        else
        {
            Instantiate(laser_Prefab, transform.position + _spawnPosition, Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void Damage()
    {
        if(_isshieldActive == true)
        {
            _isshieldActive = false;

            shields.SetActive(false);

            return;
        }

            _lives--;

        if(_lives == 2)
        {
            _rightEngine.gameObject.SetActive(true);
        }else if (_lives == 1)
        {
            _leftEngine.gameObject.SetActive(true);
        }
        _UIManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
           
            }

    }
    public void TripleShotActive()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _tripleShotActive = false;
    }

    public void SpeedUpActive()
    {
        StartCoroutine(SpeedUpPowerupRoutine());
    }
    IEnumerator SpeedUpPowerupRoutine()
    {
        _isspeedUpActive = true;
        _speed = 8.5f;

        yield return new WaitForSeconds(5);

        _isspeedUpActive = false;
        _speed = 3.0f;
    }

    public void ShieldPowerupActive()
    {
        _isshieldActive = true;

        shields.SetActive(true);
    }
    
    public void UpdateScore()
    {
        _score += 10;
    }
    public int GetScore()
    {
        return _score;
    }
    public void SpeedThruster()
    {
        _speed = _thrusterSpeed;
    }

}
