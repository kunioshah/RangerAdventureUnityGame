using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float positionMaxX;
    [SerializeField] float positionMinX;
    private float cameraStartX;
    [SerializeField] float positionMaxY;
    [SerializeField] float positionMinY;
    private float cameraStartY;
    // Start is called before the first frame update
    void Start()
    {
        cameraStartX = transform.position.x;
        cameraStartY = transform.position.y;
     
    }

    // Update is called once per frame
    void Update()
    {
        var positionX = Mathf.Max(cameraStartX, player.transform.position.x);
        var positionY = Mathf.Max(cameraStartY, player.transform.position.y);
        var positionClampedX = Mathf.Clamp(positionX, positionMinX, positionMaxX);
        var positionClampedY = Mathf.Clamp(positionY, positionMinY, positionMaxY);
        transform.position = new Vector3(positionClampedX, positionClampedY, -10f);
    }
}
