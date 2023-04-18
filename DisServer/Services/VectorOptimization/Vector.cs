namespace DisServer.Services.VectorOptimization
{
    public class Vector
    {
        public Dictionary<int, double> Values { get; set; }

        public int Weight { get; set; }

        public bool NeedMaximize { get; set; }
    }
}
