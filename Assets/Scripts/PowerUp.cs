using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 3.0f;
    [SerializeField]
    private AudioClip _clip;
  
    [SerializeField]
    private int _powerupID;// 0 is tripleshotPowerup , 1 is Speed , 2 is Shield
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down *_speed* Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
           Player player =  other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if(player != null)
            {

                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;

                    case 1:
                        player.SpeedUpActive();
                      

                        break;
                    case 2:
                        player.ShieldPowerupActive();
                       
                        break;
                    default:
                        Debug.Log("Default case");
                        break;
                }
            }
            else
            {
                Debug.LogError("Player is null");
            }

            Destroy(this.gameObject);
        }
    }
}
