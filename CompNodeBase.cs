namespace Compress
{
    abstract class CompNodeBase
    {
        public CompNode Parent { get; set; }

        public abstract int Count();
    }
}
