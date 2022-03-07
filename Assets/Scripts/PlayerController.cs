using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 _currentPosition=Vector3.zero;
    Vector3 _previousPosition= Vector3.zero;
    Vector3 _direction = Vector3.zero;

    [SerializeField]
    private float _swipeDistance = 40f;
    [SerializeField]
    private float _speed = 10f;

    bool _fingerDown = false;

    void Update()
    {
        if (!GameManager.Instance.Gameover)
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                _currentPosition = Touchscreen.current.primaryTouch.position.ReadValue();

                if (!_fingerDown)
                {
                    _previousPosition = _currentPosition;
                    _fingerDown = true;
                }

                if (Vector2.Distance(_currentPosition, _previousPosition) >= _swipeDistance)
                {
                    Vector2 direction = _currentPosition - _previousPosition;
                    direction.Normalize();
                    if (Vector2.Dot(Vector2.right, direction) > 0.9f)
                    {
                        _direction = Vector3.right;
                    }
                    else if (Vector2.Dot(Vector2.left, direction) > 0.9f)
                    {
                        _direction = Vector3.left;
                    }
                    _previousPosition = _currentPosition;
                }
            }
            else
            {
                _direction = Vector3.zero;
                _previousPosition = Vector3.zero;
                _currentPosition = Vector3.zero;
                _fingerDown = false;
            }

            if (this.transform.position.x >= -3.5 && this.transform.position.x <= 3.5)
            {
                this.transform.Translate(_direction * Time.deltaTime * _speed);
            }
            if (this.transform.position.x < -3.5)
            {
                this.transform.position = new Vector3(-3.5f, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.x > 3.5)
            {
                this.transform.position = new Vector3(3.5f, this.transform.position.y, this.transform.position.z);
            }
        }

    }

}

