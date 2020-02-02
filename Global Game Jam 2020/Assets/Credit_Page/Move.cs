using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] float velocity_x;
    [SerializeField] float velocity_y;
    [SerializeField]
    private enum Mode { left, right, up ,down, idle }
    [SerializeField]
    private Mode mode = Mode.up;

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case Mode.left: velocity_x = velocity; break;
            case Mode.right: velocity_x = -velocity; break;
            case Mode.up: velocity_y = velocity; break;
            case Mode.down: velocity_y = -velocity; break;
            default: break;

        }
        gameObject.transform.position += new Vector3(velocity_x * Time.deltaTime, velocity_y * Time.deltaTime, 0);
        gameObject.transform.position += new Vector3(velocity_x * Time.deltaTime, velocity_y * Time.deltaTime, 0);
    }
}
