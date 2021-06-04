using MeusInvestimentosApi.Models;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services
{
    public interface IInvestimentosFactory
    {
        Task<Investimento> ObterInvestimentos();
    }
}