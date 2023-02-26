using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // precisa ser calculado apenas uma vez. Posi��o da camera menos a posi��o da bola
        offset = transform.position - player.transform.position;

    }

    // Update � chamado uma vez por frame. Mas isso pode dar problemas
    // N�o d� para controlar a ordem em que as fun��es rodam no update. E se algo rodar antes?
    // Vai s� rodar depois de todos os outros updates - Late Update.
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
