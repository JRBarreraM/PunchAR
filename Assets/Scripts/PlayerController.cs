using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator animator;
    public Transform badGuy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }   


    public void DoLeftHook(){
        StartCoroutine(DoAnimation("LeftHook"));
    }
    public void DoRightHook(){
        StartCoroutine(DoAnimation("RightHook"));
    }
    public void DoRightBlock(){
        StartCoroutine(DoAnimation("RightBlock"));
    }
    public void DoLeftBlock(){
        StartCoroutine(DoAnimation("LeftBlock"));
    }
    IEnumerator DoAnimation(string action){
        animator.SetBool(action, true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool(action, false);
    }

    // Update is called once per frame
    void Update()
    {

        bool leftHook = Input.GetKey("h");
        bool rightBlock = Input.GetKey("k");
        bool leftBlock = Input.GetKey("j");
        bool rightHook = Input.GetKey("l");

        // animator.SetBool("LeftHook", leftHook);
        // animator.SetBool("LeftBlock", leftBlock);
        // animator.SetBool("RightBlock", rightBlock);
        // animator.SetBool("RightHook", rightHook);

        Vector3 targetPostition = new Vector3(badGuy.position.x, this.transform.position.y, badGuy.position.z);
        this.transform.LookAt(targetPostition);
        transform.Rotate(new Vector3(0f,15f,0f));

    }
}
