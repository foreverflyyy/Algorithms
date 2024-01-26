namespace Graph
{
    public class Node<T>
    {
        public T Name { get; set; }
        public List<Node<T>> ConnectedNodes { get; set; }
        public bool Visited { get; set; }
    }
}
