using MeusInvestimentosApi.Models;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInvestimentosFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Investimento> ObterInvestimentos();
    }
}