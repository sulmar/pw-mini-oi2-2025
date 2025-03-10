namespace TestApp
{
    public class Logger
    {
        public string LastMessage { get; set; }

        public void Log(string message)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(message);

            LastMessage = message;

            // Write the log to a storage
            // ...
        }
    }
}