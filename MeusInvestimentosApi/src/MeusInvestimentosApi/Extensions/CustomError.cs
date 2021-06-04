using Newtonsoft.Json;

namespace MeusInvestimentosApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomError
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}