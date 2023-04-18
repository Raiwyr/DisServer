namespace DisServer.Services.VectorOptimization
{
    public interface IVectorOptimization
    {
        public Dictionary<int, double> Optimize(List<Vector> vectors);
    }
}
