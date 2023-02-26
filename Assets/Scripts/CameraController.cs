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
        // precisa ser calculado apenas uma vez. Posição da camera menos a posição da bola
        offset = transform.position - player.transform.position;

    }

    // Update é chamado uma vez por frame. Mas isso pode dar problemas
    // Não dá para controlar a ordem em que as funções rodam no update. E se algo rodar antes?
    // Vai só rodar depois de todos os outros updates - Late Update.
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
