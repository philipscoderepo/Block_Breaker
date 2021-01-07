using UnityEngine;

public class Ball : MonoBehaviour
{
    //Config
    [SerializeField] Paddle paddle1 = null;
    [SerializeField] float velocityX = 1f;
    [SerializeField] float velocityY = 1f;
    [SerializeField] AudioClip[] ballSounds = null;
    [SerializeField] float randomFactor = 0.2f;

    //State
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    //Cached component refrences
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        //Ball position - paddle's position gives us the distance between the two objects
        paddleToBallVector = transform.position - paddle1.transform.position;

        //Defines what component on the object
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
       
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }

    }

    private void LaunchOnClick()
    {
        if(Input.GetMouseButton(0))
        {
            myRigidBody2D.velocity = new Vector2(velocityX, velocityY);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        if (hasStarted)
        {
            PlayAudio();
            myRigidBody2D.velocity += velocityTweak;
        }


    }

    private void PlayAudio()
    {
        AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
        myAudioSource.PlayOneShot(clip);
    }
}
