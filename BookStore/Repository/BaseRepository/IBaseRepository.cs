namespace BookStore.Repository.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Create<VM>(VM VMModel);
        Task<T> Update<VM>(VM VMModel);
        Task<bool> Delete(int id);
        void Save();
    }
}
