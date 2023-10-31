using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Working on this script: Ky'onna

public abstract class State : MonoBehaviour
{
    //Can be overriden by derived classes
    public virtual void Initialize()
    {

    }

    //Abstract because destination choosing is different for each class
    protected abstract Transform ChooseDestination();
}
