using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag== "Deactivator")
        {
            this.gameObject.SetActive(false);
        }
        if (other.transform.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
