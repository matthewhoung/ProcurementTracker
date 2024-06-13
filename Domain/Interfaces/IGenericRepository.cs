using Domain.Entities.Commons.Generic;

namespace Domain.Interfaces
{
    public interface IGenericRepository
    {
        // Create section
        Task CreateWorkerClassAsync(WorkerClass workerClass);
        Task CreateWorkerTypeAsync(WorkerType workerType);
        Task CreateWorkerTeamAsync(WorkerTeam workerTeam);
        Task CreateDepartmentAsync(Department department);
        Task CreatePayByAsync(PayBy payBy);
        Task CreatePayTypeAsync(PayType payType);
        Task CreateUnitAsync(UnitClass unitClass);
        Task CreateRolesAsync(Roles roles);
        // Read section
        Task<List<WorkerClass>> GetAllWorkerClassesAsync();
        Task<List<WorkerType>> GetAllWorkerTypesAsync();
        Task<List<WorkerTeam>> GetAllWorkerTeamsAsync();
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<List<PayBy>> GetAllPayBysAsync();
        Task<List<PayType>> GetAllPayTypesAsync();
        Task<List<UnitClass>> GetAllUnitsAsync();
        Task<List<Roles>> GetAllRolesAsync();
    }
}
