namespace Oxite.Infrastructure
{
    public class PagingInfo
    {
        public PagingInfo(int index, int size)
        {
            Index = index;
            Size = size;
        }

        public int Index { get; set; }
        public int Size { get; set; }
    }
}
