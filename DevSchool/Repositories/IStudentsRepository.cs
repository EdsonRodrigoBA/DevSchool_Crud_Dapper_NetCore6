using DevSchool.Entities;
using DevSchool.Models;

namespace DevSchool.Repositories
{
    public interface IStudentsRepository
    {
        public Task<string> Add(StudentsInputModel model);
        public Task Update(string Id,StudentsInputModel model);

        public Task<IEnumerable<Students>> GetAll();

        public Task<Students> GetById(string Id);

        public Task Delete(string Id);



    }
}
