using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _gravity = -40;
    private float _launchHight = 25;
    private Rigidbody _playerRigidbody;
    private Vector3 _targetPosition;

    void Awake()
    {
        _playerRigidbody = this.GetComponent<Rigidbody>();
    }

    void Start()
    {
        _playerRigidbody.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.Gameover)
        {
            if (transform.position.y < 0)
            {
                this.GetComponent<Renderer>().enabled = false;
                _playerRigidbody.isKinematic = true;
                GameManager.Instance.GameOver();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.Instance.Gameover)
        {
            if (collision.gameObject.tag == "Tile")
            {
                GameManager.Instance.UpdateScore(1);
                _playerRigidbody.useGravity = false;
                _playerRigidbody.velocity = Vector3.zero;

                GameObject tileObj = SpawnManager.Instance.ActiveTilePool.Dequeue();
                SpawnManager.Instance.TilePool.Enqueue(tileObj);
                GameObject nextTarget = SpawnManager.Instance.ActiveTilePool.Peek();
                _launchHight = Vector3.Distance(collision.transform.position, nextTarget.transform.position) / 2;
                _targetPosition = new Vector3(this.transform.position.x, 0, nextTarget.transform.position.z);
                Launch();
            }
        }
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * _gravity;
        _playerRigidbody.useGravity = true;
        _playerRigidbody.velocity = CalculateLaunchData();
    }

    Vector3 CalculateLaunchData()
    {
        float displacementY = _targetPosition.y - _playerRigidbody.position.y;
        Vector3 displacementXZ = new Vector3(_targetPosition.x - _playerRigidbody.position.x, 0, _targetPosition.z - _playerRigidbody.position.z);
        float time = Mathf.Sqrt(-2 * _launchHight / _gravity) + Mathf.Sqrt(2 * (displacementY - _launchHight) / _gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _gravity * _launchHight);
        Vector3 velocityXZ = displacementXZ / time;
        Vector3 launchVelocity = velocityXZ + velocityY * -Mathf.Sign(_gravity);

        return launchVelocity;
    }

}
