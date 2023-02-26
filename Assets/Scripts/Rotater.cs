using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // pode mexer um objeto ou com translate ou com rotate
        // Rotate the game object that this script is attached to by 15 in the X axis,
        // 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
        // rather than per frame.
        // deltaTime deixa as coisas smooth, ele leva em consideração a diferença entre o tempo do frameatual pro ultimo
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

    }
}
