namespace Inquiry.Core.ApplicationService.Dtos.Posts
{
    public record PostSearchCriteria(int userId=0,int id=0,string title="",string body="");
}
