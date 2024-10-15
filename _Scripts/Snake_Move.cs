using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Snake_Move : MonoBehaviour
{
    public bool isRunDone = false; // check ran quay dau ch  
   public GameManager gameManager;
   public float speed = 1.0f;
    private  Vector2 direction = Vector2.right;
    public Transform tailSnake;   
    public Transform bodySnake;
    public List<Transform> list;
    // Update is called once per frame
    private void Start()
    {
        list = new List<Transform>();
        list.Add(this.transform);  
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.S) && direction.x != 0 && isRunDone == true)
        {
            isRunDone = false ;
            direction = Vector2.down;
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Xoay 180 độ
        }
        else if (Input.GetKey(KeyCode.W) && direction.x != 0 && isRunDone == true)
        {
            isRunDone = false ;
            direction = Vector2.up;
            this.transform.rotation = Quaternion.Euler(0f, 0f, 180f); // Xoay 180 độ
        }
        else if (Input.GetKey(KeyCode.A) && direction.y != 0 && isRunDone == true)
        {
            isRunDone=false ;
            direction = Vector2.left;
            this.transform.rotation = Quaternion.Euler(0f, 0f, 270f); // Xoay 180 độ
        }
        else if (Input.GetKey(KeyCode.D) && direction.y != 0 && isRunDone == true)
        {
            isRunDone=false ;
                direction = Vector2.right;
            this.transform.rotation = Quaternion.Euler(0f, 0f, 90f); // Xoay 180 độ


        }
    }
        void FixedUpdate()
        { 
            tailSnake.position = list[list.Count-1].position;
            tailSnake.transform.rotation =  list[list.Count-1].rotation;
            for(int i =  list.Count - 1; i > 0; i--)
            {
            list[i].transform.position = list[i-1].transform.position;
            list[i].transform.rotation = list[i-1].transform.rotation;
            }
        this.transform.position = new Vector3
            {
                x = Mathf.Round(this.transform.position.x + direction.x * speed ),
                y = Mathf.Round(this.transform.position.y + direction.y * speed ),
                z = 0f
            };
        isRunDone = true;
        }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            gameManager.audio_source.clip = gameManager.GetComponent<GameManager>().getFood;
            gameManager.audio_source.PlayOneShot(gameManager.audio_source.clip);
            Transform newbody = Instantiate(bodySnake);
            newbody.position = list[list.Count - 1].position;
            list.Add(newbody);
        }
        else if (collision.tag == "Obtacles")
        {
            // Gọi hàm kết thúc trò chơi khi rắn chạm vào tường hoặc thân
            gameManager.EndGame();
        }
        else if(collision.tag == "Snake")
        {
            if(list.Count > 1)
            {
                gameManager.EndGame();
            }
        }
    }

}
