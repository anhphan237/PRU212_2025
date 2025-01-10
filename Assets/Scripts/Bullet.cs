using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] AudioClip crashBulletFSX;
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;
    bool isFired;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        //xSpeed = player.transform.localScale.x * bulletSpeed;

        // Xác định hướng ban đầu của viên đạn khi bắn
        if (player.transform.localScale.x < 0)
        {
            xSpeed = -Mathf.Abs(bulletSpeed); // Tốc độ âm để bắn sang trái
            transform.localScale = new Vector3(-1, 1, 1); // Lật viên đạn theo trục X
        }
        else
        {
            xSpeed = Mathf.Abs(bulletSpeed); // Tốc độ dương để bắn sang phải
            transform.localScale = new Vector3(1, 1, 1); // Hướng bình thường
        }

        myRigidbody.velocity = new Vector2(xSpeed, 0f); // Đặt vận tốc ban đầu
        isFired = true; // Đánh dấu rằng viên đạn đã được bắn ra
        AudioSource.PlayClipAtPoint(crashBulletFSX, Camera.main.transform.position);
    }

    void Update()
    {
        // Không cho phép điều khiển viên đạn sau khi nó đã bắn ra
        if (!isFired) return;

        // Đạn tiếp tục di chuyển theo vận tốc đã đặt, không bị thay đổi
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }            
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
