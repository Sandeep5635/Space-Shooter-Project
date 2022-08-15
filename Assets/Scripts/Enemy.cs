using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]

    private GameObject _laserPrefab;
    Player player;

    private Animator _animator;

    private AudioSource _audiosource;

    private float _firerate = 3.0f;

    private float _canfire = -1;
    

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        if(player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _animator = GetComponent<Animator>();

        _audiosource = GetComponent<AudioSource>();

        if(_audiosource == null)
        {
            Debug.LogError("AudioSource is NULL");
        }
        StartCoroutine(SpawnLaserRoutine());
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovemement();
       
        /*
                if(Time.time> _canfire)
                {
                    _firerate = Random.Range(3f, 7f);
                    _canfire =  Time.time + _firerate;

                   GameObject enemylaser =  Instantiate(_laserPrefab, transform.position + new Vector3(0, -1.6f, 0), Quaternion.identity);
                    Laser[] lasers = enemylaser.GetComponentsInChildren<Laser>();
                    for(int i = 1; i< lasers.Length; i++)
                    {

                        lasers[i].AssignEnemyLaser();
                        Debug.Log("laser assignment Started");
                    }
                }*/

    }

    IEnumerator SpawnLaserRoutine()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        Instantiate(_laserPrefab, transform.position + new Vector3(0, -1.6f, 0), Quaternion.identity);
    }

    public void CalculateMovemement()
    {

        int _spawnPositionX = Random.Range(-12, 12);

        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(_spawnPositionX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if(player!= null)
            {
                player.UpdateScore();
            }
            // here we call method
            _animator.SetTrigger("TriggerEnemyExplosion");
            _speed = 0;
            _audiosource.Play();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject , 2.8f);
        }

        if (other.CompareTag("Player"))
        {
            
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            _animator.SetTrigger("TriggerEnemyExplosion");
            _speed = 0;
            _audiosource.Play();
            Destroy(this.gameObject , 2.8f);
        }

    }

   

}
