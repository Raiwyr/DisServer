using System.Collections.Generic;

namespace DisServer.Services.VectorOptimization
{
    public class VectorOptimizationService : IVectorOptimization
    {
        public Dictionary<int, double> Optimize(List<Vector> vectors)
        {
            try
            {

                if (vectors.Select(v => v.Values.Count()).Distinct().Count() != 1)
                    throw new Exception("The сount value for Vector.Values must be the same for all items in the collection");
                else
                {
                    var v = vectors.Select(v => v.Values.Keys).ToList();
                    List<int> unionList = new();
                    v.ForEach(n => unionList = unionList.Union(n).ToList());

                    if (v[0].Count != unionList.Count)
                        throw new Exception("The unique key values must be the same for all Vectors.Values in the list");
                }

                Dictionary<int, double> result = new();
                List<Dictionary<int, double>> dicts = new();

                foreach (var v in vectors)
                    if (v.NeedMaximize)
                        dicts.Add(Maximize(v.Values, v.Weight));
                    else
                        dicts.Add(Minimize(v.Values, v.Weight));

                foreach (var dict in dicts)
                {
                    if (result.Count == 0)
                        result = dict;
                    else
                        foreach (var key in result.Keys)
                            result[key] += dict[key];
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        private Dictionary<int, double> Maximize(Dictionary<int, double> dict, int weight)
        {
            try
            {
                Dictionary<int, double> minimieDict = new();
                foreach (var i in dict)
                    minimieDict.Add(i.Key, (i.Value - dict.Values.Min()) / (dict.Values.Max() - dict.Values.Min()) * weight);
                return minimieDict;
            }
            catch
            {
                throw;
            }
        }

        private Dictionary<int, double> Minimize(Dictionary<int, double> dict, int weight)
        {
            try
            {
                Dictionary<int, double> minimieDict = new();
                foreach (var i in dict)
                    minimieDict.Add(i.Key, (dict.Values.Max() - i.Value) / (dict.Values.Max() - dict.Values.Min()) * weight);
                return minimieDict;
            }
            catch
            {
                throw;
            }
        }
    }
}
