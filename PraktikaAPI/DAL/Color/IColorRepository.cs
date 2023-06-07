using PraktikaAPI.Models;

namespace PraktikaAPI.DAL
{
    public interface IColorRepository
    {
        IEnumerable<Color> GetColors();
        Color? GetColorByID(int colorId);
        void InsertColor(Color color);
        void DeleteColor(int colorId);
        void UpdateColor(Color color);
    }
}
