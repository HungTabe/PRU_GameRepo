using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        } else
        {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            // Đặt vận tốc của đối tượng về 0, tức là dừng lại ngay lập tức.
            _rigidbody.velocity = Vector3.zero;
            // Dừng xoay của đối tượng, không cho nó tiếp tục xoay nữa.
            _rigidbody.angularVelocity = 0.0f;
            // Tắt (ẩn) đối tượng trong game, khiến nó không còn xuất hiện hoặc tương tác nữa.
            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }


}
