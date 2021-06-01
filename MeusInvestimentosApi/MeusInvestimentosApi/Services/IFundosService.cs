using MeusInvestimentosApi.Models;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services
{
    public interface IFundosService
    {
        Task<Investimento> ObterFundos();
    }
}