using AYA_UIS.Core.Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Departments { get; }
        ICourseRepository Courses { get; }
        IAcademicScheduleRepository AcademicSchedules { get; }
        IDepartmentFeeRepository DepartmentFees { get; }
        IFeeRepository Fees { get; }
        IStudyYearRepository StudyYears { get; }
        IRegistrationRepository Registrations { get; }
        ICourseUploadsRepository CourseUploads { get; }
        ISemesterRepository Semesters { get; }

        Task<int> SaveChangesAsync();

        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntities<TKey>;
    }
}
