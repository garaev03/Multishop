namespace MultiShop.Services.Interfaces
{
    public interface IColorService
    {
        public List<List<int>> SplitColors(List<int> ColorIds);
        public void CheckColors(List<List<int>> Colors, int SizeCount);
    }
}
