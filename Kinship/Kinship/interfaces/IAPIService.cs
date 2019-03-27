using System.Threading.Tasks;
using Refit;

namespace Kinship.interfaces
{
    public interface IAPIService
    {
        [Get("/posts/1")]
        Task<string> GetPost();
        [Get("/todos/1")]
        Task<string> GetToDo();
    }
}
