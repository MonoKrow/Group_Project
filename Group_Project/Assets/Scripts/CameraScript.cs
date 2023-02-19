using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float moveStrength;
    public Vector2 offset;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameMannager.instance.playerObject.transform;
        transform.position = new Vector3(playerTransform.position.x + offset.x, playerTransform.position.y + offset.y, transform.position.z);
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y == -1 && Camera.main.orthographicSize < 1.95f)
        {
            Camera.main.orthographicSize += 0.1f;
        }
        if (Input.mouseScrollDelta.y == 1 && Camera.main.orthographicSize > 1.05f)
        {
            Camera.main.orthographicSize -= 0.1f;
        }

        Vector3 temp;
        if (Input.GetKey(KeyCode.F))
        {
            float xDistance = (Input.mousePosition.x - (Screen.width * 0.5f));
            float yDistance = (Input.mousePosition.y - (Screen.height * 0.5f));

            if (xDistance > Screen.width * 0.5f)
            {
                xDistance = Screen.width * 0.5f;
            }
            else if (xDistance < -(Screen.width * 0.5f))
            {
                xDistance = -(Screen.width * 0.5f);
            }

            if (yDistance > Screen.height * 0.5f)
            {
                yDistance = Screen.height * 0.5f;
            }
            else if (yDistance < -(Screen.height * 0.5f))
            {
                yDistance = -(Screen.height * 0.5f);
            }

            temp = playerTransform.position - (transform.position - ((new Vector3(xDistance / Screen.width, yDistance / Screen.height, 0)) * (1.5f * Camera.main.orthographicSize)));
        }
        else
        {
            temp = playerTransform.position - (transform.position - new Vector3(offset.x, offset.y, 0));
        }
        temp = new Vector3(temp.x, temp.y, 0);
        transform.position += temp * moveStrength * Time.deltaTime;
    }
}
