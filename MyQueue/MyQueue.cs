using System.Collections;

namespace MyQueue;

public class MyQueue<T> : IEnumerable<T>, ICollection
{

    public Node<T>? _head;
    public Node<T>? Head => _head;
    
    public Node<T>? _tail;
    public Node<T>? Tail => _tail;
    
    private int _size;
    public int Count => _size;

    public MyQueue()
    {
        _head = null;
        _tail = null;
        _size = 0;
    }

    public MyQueue(IEnumerable<T> collection)
    {
        if (collection is null)
        {
            throw new ArgumentNullException();
        }

        foreach (T obj in collection)
        {
            Enqueue(obj);
        }
    }
    
    public void CopyTo(Array array, int index)
    {
        if (array == null)
        {
            throw new ArgumentNullException();
        }

        if (index < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        Node<T> currentNode = _head;
        if (currentNode == null)
        {
            throw new InvalidOperationException();
        }

        int size = index + _size;
        if (size > array.Length)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        for (var i = index; i <= size; i++)
        {
            array.SetValue(currentNode.Data, i);
            currentNode = currentNode.Next;
            
            if (currentNode == null)
            {
                break;                
            }
        }
    }
    
    public event EventHandler<QueueArgs>? Add;
    public event EventHandler<QueueArgs>? Delete;
    public event EventHandler<QueueArgs>? Clear;

    public void Enqueue(T data)
    {
        Node<T> node = new Node<T>(data);

        if (_head == null)
        {
            _head = node;
        }
        else
        {
            _tail.Next = node;
            //node.Previous = _tail;
        }

        _tail = node;
        _size++;
        
        Add?.Invoke(this, new QueueArgs("The element was enqueued."));
    }
    
    public T Peek()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException();
        }

        return _head.Data;
    }

    public T Dequeue()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException();
        }

        T data = _head!.Data;

        if (_size == 1)
        {
            _head = _tail = null;
        }
        else
        {
            _head = _head.Next;
            //_head!.Previous = null;
        }

        _size--;
        
        Delete?.Invoke(this, new QueueArgs($"The element was dequeued."));
        
        if (_size == 0)
        {
            Clear?.Invoke(this, new QueueArgs("The queue is cleared."));
        }

        return data;
    }

    public bool Contains(T data)
    {
        Node<T>? currentNode = _head;

        while (currentNode != null)
        {
            if (currentNode.Data.Equals(data))
            {
                return true;
            }

            currentNode = currentNode.Next;
        }
        return false;
    }

    public void ClearQueue()
    {
        _head = null;
        _tail = null;
        _size = 0;

        Clear?.Invoke(this, new QueueArgs("The queue is cleared."));
    }

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => this;
    
    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? currentNode = _head;
        while (currentNode != null)
        {
            yield return currentNode.Data;
            currentNode = currentNode.Next;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}