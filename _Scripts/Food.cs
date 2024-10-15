using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Food : MonoBehaviour
{
    public int score;
    private Snake_Move snake_move;
    // Start is called before the first frame update
    void Start()
    {
        snake_move = GameObject.FindGameObjectWithTag("Head").GetComponent<Snake_Move>();
        // Đặt vị trí ngẫu nhiên khi bắt đầu trò chơi
        RandomPosition();
        score = 0;
    }

    // Đặt đối tượng tại vị trí ngẫu nhiên trong phạm vi nhất định
    void RandomPosition()
    {
        int x, y;
        while (true)
        {
            x = Random.Range(-13, 13);
            y = Random.Range(-4, 4);
            if (checkPositon(x, y))
            {
                break;
            }
        }
            
        this.transform.position = new Vector3(x, y, 0);
    }

    // Kiểm tra va chạm với "Head"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Head")
        {
            // Nếu va chạm với "Head", thay đổi vị trí của đối tượng
            RandomPosition();
            score++;
        }
    }
    bool checkPositon(int x, int y)
    {
        for (int i = 0; i < snake_move.list.Count; i++)
        {
            if (snake_move.list[i].transform.position.x != x || snake_move.list[i].transform.position.y != y)
            {
                return true;
            }
        }
        if(snake_move.tailSnake.transform.position.x != x || snake_move.tailSnake.transform.position.y != y) return true;
        return false;
    }
   
}
