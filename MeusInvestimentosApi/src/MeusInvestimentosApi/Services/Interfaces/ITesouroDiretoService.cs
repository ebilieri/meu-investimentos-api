using MeusInvestimentosApi.Models;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITesouroDiretoService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Investimento> ObterTesouroDireto();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Investimento> ObterTesouroDiretoCalculado();
    }
}