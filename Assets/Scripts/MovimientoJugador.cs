using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidadGiro = 120f;
    public float velocidadAvance = 5f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
       
        float ejeHorizontal = Input.GetAxis("HorizontalTeclado");
        float ejeVertical = Input.GetAxis("VerticalTeclado");

        float angulo = ejeHorizontal * velocidadGiro * Time.deltaTime;
        transform.Rotate(0f, angulo, 0f);

        
        Vector3 movimientoLocal = new Vector3(0f, 0f, ejeVertical * velocidadAvance);

        Vector3 movimientoMundo = transform.TransformDirection(movimientoLocal);

        controller.Move(movimientoMundo * Time.deltaTime);
    }
   

}
