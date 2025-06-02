using System.Globalization;
using System.Reactive.Linq;
using System.Reactive.Subjects;

Console.WriteLine("Hello, Reactive Programming!");

// Rx-Lib = Reactive Extensions Library
// Biblioteka z implementacją paradygmatu programowania reaktywnego


// Pull: IEnumerable + Linq 
// Push: IObservable (events) + Linq = Rx Lib

// nadajnik
// SimpleStream source = new SimpleStream();

ReplaySubject<int> source = new ReplaySubject<int>();

// odbiorniki
IObserver<int> observer1 = new ConsoleObserver("Marcel");
IObserver<int> observer2 = new ConsoleObserver("Radek");

source.Subscribe(observer2);


// var paired = source.Buffer(3,2);

var paired = source.Buffer(TimeSpan.FromSeconds(2));
    
paired.Subscribe(items => Console.WriteLine($"[Buffer] {string.Join(", ", items)}"));


// source.Push(1);
source.OnNext(1);

await Task.Delay(1000);

source.OnNext(10);

await Task.Delay(1000);

source.OnNext(20);

await Task.Delay(1000);


// source.Push(1);
source.OnNext(1);

await Task.Delay(1000);


var filteredSource = source.Where(item => item > 2);
   
filteredSource    
    .Subscribe(observer2);

// source.Push(2);
source.OnNext(2);

await Task.Delay(2000);

// source.Push(3);
source.OnNext(30);

await Task.Delay(1500);

// source.Push(-1);
source.OnError(new Exception("Value must be greater than 0"));

await Task.Delay(100);

// source.Push(4);
source.OnNext(4);

class ConsoleObserver(string _name) : IObserver<int>
{
    public void OnCompleted()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[{_name} Completed");
        Console.ResetColor();
    }

    public void OnError(Exception error)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{_name}] {error.Message}");
        Console.ResetColor();
    }

    public void OnNext(int value)
    {
        Console.WriteLine($"[{_name}] {value}");
    }
}

class SimpleStream : IObservable<int>
{
    private List<IObserver<int>>? _observers = [];
    private List<int> history = new List<int>();
    
    public IDisposable Subscribe(IObserver<int> observer)
    {
        _observers.Add(observer);

        foreach (var item in history)
        {
            observer.OnNext(item);
        }

        return new DummyDisposable();
    }

    public void Push(int value)
    {
        history.Add(value);
        
        if (value <= 0)
            foreach (var observer in _observers)
            {
                observer.OnError(new Exception("Value must be greater than 0"));
            }
        else
            foreach (var observer in _observers)
            {
                observer.OnNext(value);
            }
    }
    
    private class DummyDisposable : IDisposable
    {
        public void Dispose()
        {
        }
    }
}
