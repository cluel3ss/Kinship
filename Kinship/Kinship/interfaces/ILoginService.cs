using System.Threading.Tasks;
using Refit;

namespace Kinship.interfaces
{
    public interface ILoginService
    {
        [Get("/todos/1")]
        Task<string> LoginUser();
        [Get("/posts/1")]
        Task<string> LogoutUser();
    }
}
