using UnityEngine;
using System.Collections;
using System;

public class AnimationTrigger : MonoBehaviour
{
    private Animator _animator;

    // Random must be static in order to generate unique values for each instance
    private static readonly System.Random Random = new System.Random();

    // Use this for initialization
    void Start ()
	{
        // Getting the animator component and storing it in a private field
	    _animator = GetComponent<Animator>();

        // Starting a new coroutine which would not block main game loop
	    StartCoroutine(PlayAnimation());
	}

    private IEnumerator PlayAnimation()
    {
        // Set random amount of seconds after which the animation will start
        int range = 10;
        float seconds = (float) Random.NextDouble() * range;

        // Suspending the execution for given time
        yield return new WaitForSeconds(seconds);

        // Triggering the animation
        _animator.SetBool("isSwaying", true);
    }

    // Update is called once per frame
    void Update () {
	    // nothing here for now.. 
	}
}
