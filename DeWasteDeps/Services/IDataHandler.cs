using DeWaste.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace DeWaste.Services
{
    public interface IDataHandler
    {
        Task<Item> GetItemById(int id);
        Task<ObservableCollection<Suggestion>> GetSimilar(string name);
        Task<ObservableCollection<Comment>> GetCommentsByItemID(int id);
        Task<Comment> SubmitComment(int item_id, string content);
        Task<Comment> DeleteComment(int id);
        Task<Comment> LikeComment(Comment comment);
        Task<Comment> DislikeComment(Comment comment);
    }
}
