using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public Transform targetObject;
    public Vector3 offset;
    [HideInInspector] public Canvas canvas;
    public Vector3 screenPosition;
    private TMP_Text textObject;
    [HideInInspector] public int damage;

    private Vector3 objectPosition;

    public float lifetime = 2f;
    private float timer = 0f;

    public float transition;
    private float randomRotation;
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        
        textObject = transform.GetChild(0).GetComponent<TMP_Text>();
        textObject.text = damage.ToString();

        randomRotation = Random.Range(-45f, 45f);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, randomRotation);
        objectPosition = targetObject.position + offset;
    }

    private void Update()
    {
        //Damage Number Animation
        if(timer >= lifetime)
        {
            Destroy(gameObject);
        }else{
            timer += Time.deltaTime;
        }

        transition = Mathf.Lerp(1, 0, timer / lifetime);

        textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, transition);



        
        Vector3 rotateOffset = Quaternion.Euler(0, 0, randomRotation) * new Vector3(0, 2, 0);

        Vector3 lerpedPosition = Vector3.Lerp(objectPosition + rotateOffset, objectPosition, transition);

        screenPosition = Camera.main.WorldToScreenPoint(lerpedPosition);
        gameObject.transform.position = screenPosition;
        

        if(screenPosition.z < 0) //when the object is off screen it sometimes shows up on screen.
        {
            textObject.enabled = false;
        }
        else
        {
            textObject.enabled = true;
        }

        


    }
}