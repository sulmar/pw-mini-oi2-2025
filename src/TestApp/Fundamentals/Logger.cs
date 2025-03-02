namespace TestApp
{
    public class Logger
    {
        public string LastMessage { get; set; }

        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException();

            LastMessage = message;

            // Write the log to a storage
            // ...
        }
    }
}