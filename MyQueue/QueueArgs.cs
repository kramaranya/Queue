namespace MyQueue;

public class QueueArgs : EventArgs
{
    public QueueArgs(string message)
    {
        Message = message;
    }

    public string Message { get; }
}