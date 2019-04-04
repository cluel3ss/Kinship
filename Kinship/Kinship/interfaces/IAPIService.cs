using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Kinship.models.requests;
using Kinship.models.responses;
using Refit;

namespace Kinship.interfaces
{
    public interface IAPIService
    {
        [Get("/databases/{db}/collections/{collection}?apiKey={apiKey}")]
        Task<ObservableCollection<Event>> GetEventList(string db, string collection, string apiKey);

        [Get("/databases/{db}/collections/{collection}?apiKey={apiKey}")]
        Task<ObservableCollection<Issue>> GetIssueList(string db, string collection, string apiKey);

        [Post("/databases/{db}/collections/{collection}?apiKey={apiKey}")]
        Task<NewRecordResponse> InsertNewIssue(string db, string collection, string apiKey, [Body] InsertIssues body);                                                                                                                                                                                                                                                                                                                                                                                                                                                             

        [Post("/databases/{db}/collections/{collection}?apiKey={apiKey}")]
        Task<Event> InsertNewEvent(string db, string collection, string apiKey, [Body] InsertEvent body);

        [Put("/databases/{db}/collections/{collection}?apiKey={apiKey}&q={query}")]
        Task<UpdateIssuesResponse> UpdateIssue(string db, string collection, string apiKey, string query, [Body] UpdateIssues body);
    }
}
