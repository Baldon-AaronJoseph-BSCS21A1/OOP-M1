using WinFormsApp1.Repositories;

namespace WinFormsApp1
{
    public static class UnitOfWork
    {
        static IStudentRepository? repository;
        public static IStudentRepository StudentRepository
        {
            get
            {
                repository ??= new StudentRepository();

                return repository;
            }
        }
    }
}
