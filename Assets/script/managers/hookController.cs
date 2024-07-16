using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookController : MonoBehaviour
{
    private GameObject hook;
    public void setHook(GameObject _hook){
        hook=_hook;
    }
    public void enableHookTrigger(){
        if(hook==null)return;
        hook.GetComponent<Collider2D>().enabled=true;
    }
    public IEnumerator EnableHookDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        enableHookTrigger();
    }
}
