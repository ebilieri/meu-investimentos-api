using MeusInvestimentosApi.Models;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services
{
    public interface IRendaFixaService
    {
        Task<Investimento> ObterRendaFixa();
    }
}