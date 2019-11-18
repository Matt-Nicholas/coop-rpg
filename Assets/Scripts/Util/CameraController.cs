using UnityEngine;

public class CameraController:SingletonPersistant<CameraController>
{ 


    void Start()
    {
        //MoveToNextTile(WorldManager.Instance.ActiveTileKey);
    }

    public void MoveToNextTile(Screen screen)
    {
        //Debug.Log("Moving to tile: " + screen.tileKey);
        //transform.position = new Vector3(screen.transform.position.x,
        //                                 screen.transform.position.y,
        //                                 transform.position.z);
    }

    public void Move(Vector2 v2)
    {

        transform.position = new Vector3(transform.position.x + v2.x,
                                         transform.position.y + v2.y,
                                         transform.position.z);
    }

    void Update()
    {
        //float x = transform.position.x;
        //float y = transform.position.y;
        
        //if(Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    x += 32;
        //}

        //else if(Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    y -= 32;
        //}

        //else if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    x -= 32;
        //}

        //else if(Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    y += 32;
        //}

        //transform.position = new Vector3(x, y, transform.position.z);

    }

}
