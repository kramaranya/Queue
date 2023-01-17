namespace MyQueue;

public class Node<T>
{
    public T Data { get; }
    public Node<T> Next { get; set; }
    //public Node<T> Previous { get; set; }
    
    public Node(T data)
    {
        Data = data;
    }

}