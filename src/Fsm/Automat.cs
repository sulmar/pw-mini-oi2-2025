﻿using System.IO.Enumeration;
using Stateless;
using Stateless.Graph;

namespace Fsm;

public class Automat
{
    public string Name { get; set; }
    
    // dotnet add package stateless
    public State CurrentState => _stateMachine.State;
    
    private StateMachine<State, Trigger> _stateMachine;
    
    public decimal TotalAmount {
        get;
       private set;
    }
    
    public decimal CurrentAmount { get; private set; }
    
    public string Graph => MermaidGraph.Format(_stateMachine.GetInfo());

    public static TimeSpan ElapsedTime => TimeSpan.FromSeconds(3);
    
    private PeriodicTimer _timer = new PeriodicTimer(ElapsedTime);
    
    private  CancellationTokenSource _cts = new CancellationTokenSource();
    
    public Automat()
    {
        TotalAmount = 5m;
        
        _stateMachine = new StateMachine<State, Trigger>(State.Idle);
        
        _stateMachine.Configure(State.Idle)
            .Permit(Trigger.Select, State.Selected);

        _stateMachine.Configure(State.Selected)
            .OnEntry(() => StartTimeoutTimer()) // Uruchom odliczanie czasu
            .OnExit(() => _cts.Cancel()) // Anuluj poprzedni timer przy wychodzeniu ze stanu
            .PermitIf(Trigger.InsertCoin, State.Processing, () => CurrentAmount >= TotalAmount, "Kwota zebrana")
            .PermitReentryIf(Trigger.InsertCoin, () => CurrentAmount < TotalAmount, "Niepełna kwota")
            .Permit(Trigger.InsertCard, State.Processing)
            .Permit(Trigger.Timeout, State.Idle)
            .Permit(Trigger.Cancel, State.Idle);

        _stateMachine.Configure(State.Processing)
            .OnEntry(() => Console.WriteLine("Processing..."), "Uruchomienie silnika")
            .Permit(Trigger.Confirm, State.Idle)
            .OnExit(() => Console.WriteLine("Processing done!"), "Wyświetlenie komunikatu");
        
        // hint: W celu pozbycia się zależności od zewnętrznych klas użyj wzorca Mediator

    }
    
    private void StartTimeoutTimer()
    {
        _cts = new CancellationTokenSource();

        Task.Run(async () =>
        {
            try
            {
                await Task.Delay(ElapsedTime, _cts.Token);
                
                if (!_cts.Token.IsCancellationRequested) // Jeśli nie anulowano
                {
                    Timeout();  // Uruchom wyzwalacz informujący o przekroczeniu limitu czasu
                }
            }
            catch (TaskCanceledException) { /* ignoruj */ }
        });
    }

    public void Select(int productId)
    {
        _stateMachine.Fire(Trigger.Select);
    }
    
    public void InsertCoin(decimal amount)
    {
        CurrentAmount += amount;
        _stateMachine.Fire(Trigger.InsertCoin);
    }

    public void InsertCard()
    {
        _stateMachine.Fire(Trigger.InsertCard);
    }
    
    public void Cancel() => _stateMachine.Fire(Trigger.Cancel);
    
    private void Timeout() => _stateMachine.Fire(Trigger.Timeout);
    
    
}

public enum State
{
    Idle,
    Selected,
    Processing
}

public enum Trigger
{
    Select,
    InsertCoin,
    InsertCard,
    Timeout,
    Cancel,
    Confirm
}


