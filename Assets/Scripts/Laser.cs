using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8.0f;

 


    // Update is called once per frame
    void Update()
    {
      

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
  

   
}
