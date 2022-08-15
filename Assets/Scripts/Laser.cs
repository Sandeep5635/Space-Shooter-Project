using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8.0f;

    [SerializeField]
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
      /*  if (_isEnemyLaser == true)
        {

            MoveDown();
            Debug.Log("Move Down Called");
        }
        else
        {
           
        }*/

        MoveUp();


    }

    public void MoveUp()
    {
        Debug.Log("MoveUp Called");
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 8)
        {
            if (transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }
    /*  public void MoveDown()
      {
          Debug.Log("MoveDown Called");

          transform.Translate(Vector3.down * _speed * Time.deltaTime);

          if (transform.position.y < -8)
          {
              if (transform.parent != null)
              {
                  Destroy(this.transform.parent.gameObject);
              }

              Destroy(gameObject);
          }
      }

      public void AssignEnemyLaser()
      {
          _isEnemyLaser = true;
          Debug.Log("Assign called");

      }*/

  /*  private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
        }
    }*/

   
}
