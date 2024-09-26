using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine _stateMachine { get; private set; }

    public Animator _animator {  get; private set; }

    private void Awake()
    {
        _stateMachine = new PlayerStateMachine();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        //TODO init
    }

    private void Update()
    {
        _stateMachine.CurrentState.Update();
    }
}
