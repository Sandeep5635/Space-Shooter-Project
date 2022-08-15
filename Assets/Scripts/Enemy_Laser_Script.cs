using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Laser_Script : MonoBehaviour
{
    private float speed = 6.0f;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -5.5)
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.Damage();

            Destroy(this.gameObject);
        }
    }
}
