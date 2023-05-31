using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(Bots bot);
    void OnExecute(Bots bot);
    void OnExit(Bots bot);
}
