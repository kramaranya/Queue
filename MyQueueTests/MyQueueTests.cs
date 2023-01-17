using System.Collections;
using NUnit.Framework;
using MyQueue;

namespace MyQueueTests;

[TestFixture]
public class MyQueueTests
{
    
    [Test]
    public void MyQueue_CreateEmptyQueue()
    {
        var queue = new MyQueue<int>();
        
        var actualHead = queue.Head;
        var actualTail = queue.Tail;
        var actualSize = queue.Count;
        
        Assert.Multiple(() =>
        {
            Assert.That(actualHead, Is.EqualTo(null), 
                "There must be no head in an empty queue.");
            Assert.That(actualTail, Is.EqualTo(null), 
                "There must be no tail in an empty queue.");
            Assert.That(actualSize, Is.EqualTo(0), 
                "The size of an empty queue must be 0.");
        });
    }
    
    [Test]
    public void MyQueue_CollectionIsNull_ThrowArgumentNullException()
    {
        string[]? collection = null;
        var expectedException = typeof(ArgumentNullException);

        var actualException = Assert.Catch(() => new MyQueue<string>(collection));
        
        Assert.That(actualException?.GetType(), Is.EqualTo(expectedException), 
            "Must throw ArgumentNullException when collection is null.");
    }

    [TestCase(new[] { 1, 2, 3 })]
    [TestCase(new[] { 1, 2 })]
    [TestCase(new[] { 1 })]
    public void MyQueue_CreateQueueFromCollection_CreateQueueCorrectly(int[] expected)
    {
        var actual = new MyQueue<int>(expected);
        
        CollectionAssert.AreEqual(expected, actual, 
            "Creating Queue from existing collection works incorrectly.");
    }

    [Test]
    public void CopyTo_WhenArrayIsNull_ThrowArgumentNullException()
    {
        var array = new[] { 1, 2 };
        var queue = new MyQueue<int>(array);
        int[]? collection = null;
        var expected = typeof(ArgumentNullException);
        
        var actual = Assert.Catch(() => queue.CopyTo(collection,0));
        
        Assert.That(actual?.GetType(), Is.EqualTo(expected), 
            "Must throw ArgumentNullException when the array is null.");
    }
    
    [Test]
    public void CopyTo_WhenIndexIsLessThanZero_ThrowArgumentOutOfRangeException()
    {
        var array = new[] { 1, 2 };
        var queue = new MyQueue<int>(array);
        int[] collection = {1, 6};
        var expected = typeof(ArgumentOutOfRangeException);
        
        var actual = Assert.Catch(() => queue.CopyTo(collection,-3));
        
        Assert.That(actual?.GetType(), Is.EqualTo(expected), 
            "Must throw ArgumentOutOfRangeException when the index is less than zero.");
    }
    
    [Test]
    public void CopyTo_WhenHeadIsNull_ThrowInvalidOperationException()
    {
        var queue = new MyQueue<int>();
        int[] collection = {1, 6};
        var expected = typeof(InvalidOperationException);
        
        var actual = Assert.Catch(() => queue.CopyTo(collection,0));
        
        Assert.That(actual?.GetType(), Is.EqualTo(expected), 
            "Must throw InvalidOperationException when the queue is empty.");
    }
    
    [Test]
    public void CopyTo_WhenArrayIsTooBigToCopyIt_ThrowArgumentOutOfRangeException()
    {
        var array = new[] { 1, 2 };
        var queue = new MyQueue<int>(array);
        int[] collection = {1, 6, 5, 2};
        var expected = typeof(ArgumentOutOfRangeException);
        
        var actual = Assert.Catch(() => queue.CopyTo(collection,7));
        
        Assert.That(actual?.GetType(), Is.EqualTo(expected), 
            "Must throw ArgumentOutOfRangeException when the array is too big for copying.");
    }
    
    [TestCase(new[] { 4, 5, 6, 7})]
    [TestCase(new[] { 4 })]
    public void CopyTo_CorrectArrayCopying_CopyCollectionToArray(int[] expected)
    {
        var queue = new MyQueue<int>(expected);
        var actual = new int[queue.Count];
        
        queue.CopyTo(actual, 0);

        CollectionAssert.AreEqual(expected, actual, 
            "Copying collection to array works incorrectly.");
    }
    
    [TestCase(new[] { 4, 5, 6, 7})]
    [TestCase(new[] { 4 })]
    public void Enqueue_AddElementToQueue(int[] expected)
    {
        var actual = new MyQueue<int>();
        foreach (var i in expected)
        {
            actual.Enqueue(i);
        }

        CollectionAssert.AreEqual(expected, actual, 
            "Adding element to queue works incorrectly");
    }

    [Test]
    public void Peek_WhenQueueIsEmpty_ThrowInvalidOperationException()
    {
        var queue = new MyQueue<int>();
        var expected = typeof(InvalidOperationException);
        
        var actual = Assert.Catch(() => queue.Peek());
        
        Assert.That(actual?.GetType(), Is.EqualTo(expected), 
            "Must throw InvalidOperationException when the queue is empty.");
    }

    [TestCase(4, new[] { 4, 5, 6, 7 })]
    [TestCase(2, new[] { 2 })]
    public void Peek_CorrectPeeking_ReturnTheFirstElementOfQueue(int expected, int[] array)
    {
        var queue = new MyQueue<int>(array);
        
        var actual = queue.Peek();
        
        Assert.That(actual, Is.EqualTo(expected), 
            "Peeking works incorrectly");
    }

    [Test]
    public void Dequeue_WhenQueueIsEmpty_ThrowInvalidOperationException()
    {
        var queue = new MyQueue<int>();
        var expected = typeof(InvalidOperationException);
        
        var actual = Assert.Catch(() => queue.Dequeue());
        
        Assert.That(actual?.GetType(), Is.EqualTo(expected), 
            "Must throw InvalidOperationException when the queue is empty.");
    }
    
    [TestCase(4, new[] { 4, 5, 6, 7 })]
    [TestCase(2, new[] { 2 })]
    public void Dequeue_DeleteFirstElementFromQueue_ReturnDeletedElement
        (int expected, int[] array)
    {
        var queue = new MyQueue<int>(array);

        var actual = queue.Dequeue();
        
        Assert.That(actual, Is.EqualTo(expected), 
            "Returning deleted element from the queue works incorrectly");
    }
    
    [TestCase(new[] { 5, 6, 7 }, new[] { 4, 5, 6, 7 })]
    [TestCase(new[] { 3 },  new[] { 2 , 3})]
    public void Dequeue_DeleteFirstElementFromQueue_RemovesFirstElementCorrectly
        (int[]? expected, int[] array)
    {
        var actual = new MyQueue<int>(array);

        actual.Dequeue();
        
        CollectionAssert.AreEqual(expected, actual, 
            "Removing the first element from queue works incorrectly");
    }
    
    [Test]
    public void Dequeue_DeleteLastElementFromQueue_RemovesLastElementCorrectly()
    {
        var queue = new MyQueue<int>();
        queue.Enqueue(1);

        queue.Dequeue();
        var headIsNull = queue.Head == null;
        var tailIsNull = queue.Tail == null;
        var countIsZero = queue.Count == 0;
        
        Assert.Multiple(() =>
        {
            Assert.IsTrue(headIsNull, "Head must be null after deleting the last element from queue");
            Assert.IsTrue(tailIsNull, "Tail must be null after deleting the last element from queue");
            Assert.IsTrue(countIsZero, "Size must be 0 after deleting the last element from queue");
        });
    }

    [Test]
    public void Contains_CheckTheElementIsInTheQueue_ReturnTrue()
    {
        var array = new[] { 1, 6, 0 };
        var queue = new MyQueue<int>(array);

        var actual = queue.Contains(6);
        
        Assert.IsTrue(actual, "It must return true when the element is in the queue.");
    }
    
    [Test]
    public void Contains_CheckTheElementIsNotInTheQueue_ReturnFalse()
    {
        var array = new[] { 1, 6, 0 };
        var queue = new MyQueue<int>(array);

        var actual = queue.Contains(2);
        
        Assert.IsFalse(actual, "It must return false when the element is not in the queue.");
    }

    [Test]
    public void ClearQueue_ClearQueue()
    {
        var collection = new[]{13, 53, 6, 0};
        var queue = new MyQueue<int>(collection);
        
        queue.ClearQueue();
        var headIsNull = queue.Head == null;
        var tailIsNull = queue.Tail == null;
        var countIsZero = queue.Count == 0;
        
        Assert.Multiple(() =>
        {
            Assert.IsTrue(headIsNull, "Head must be null after clearing the queue");
            Assert.IsTrue(tailIsNull, "Tail must be null after clearing the queue");
            Assert.IsTrue(countIsZero, "Size must be 0 after clearing the queue");
        });
    }
    
    [Test]
    public void IsSynchronized_False()
    {
        var queue = new MyQueue<int>();

        var isSynchronized = ((ICollection)queue).IsSynchronized;
        
        Assert.IsFalse(isSynchronized);
    }

    [Test]
    public void SyncRoot_This()
    {
        var queue = new MyQueue<int>();

        var syncRoot = ((ICollection)queue).SyncRoot;
        
        Assert.IsTrue(syncRoot == queue);
    }
}