using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Deactivator")
        {
            SpawnManager.Instance.SpawnFloor();
            GameObject floorObj = this.transform.parent.gameObject;
            floorObj.SetActive(false);
            SpawnManager.Instance.FloorPool.Enqueue(floorObj);
        }
    }
}
