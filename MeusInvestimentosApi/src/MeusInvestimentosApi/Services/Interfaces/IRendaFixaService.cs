using MeusInvestimentosApi.Models;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRendaFixaService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Investimento> ObterRendaFixa();
    }
}