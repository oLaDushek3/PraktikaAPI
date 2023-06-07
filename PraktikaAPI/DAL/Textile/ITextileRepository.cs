using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface ITextileRepository
    {
        IEnumerable<Textile> GetTextiles();
        Textile? GetTextileByID(int textileId);
        void InsertTextile(Textile textile);
        void DeleteTextile(int textileId);
        void UpdateTextile(Textile textile);
    }
}
