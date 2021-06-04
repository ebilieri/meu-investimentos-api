using MeusInvestimentosApi.Models;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFundosService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Investimento> ObterFundos();
    }
}