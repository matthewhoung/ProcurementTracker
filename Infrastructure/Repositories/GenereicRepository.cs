using Dapper;
using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using System.Data;

namespace Infrastructure.Repositories
{
    public class GenereicRepository : IGenericRepository
    {
        private readonly IDbConnection _dbConnection;

        public GenereicRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task CreateDepartmentAsync(Department department)
        {
            var writeCommand = @"
                INSERT INTO departments
                    (department_id, department_name)
                VALUES
                    (@DepartmentId, @DepartmentName)";
            var parameters = new 
            { 
                DepartmentId = department.DepartmentId, 
                DepartmentName = department.DepartmentName 
            };
            await _dbConnection.ExecuteAsync(writeCommand, parameters);
        }

        public async Task CreatePayByAsync(PayBy payBy)
        {
            var writeCommand = @"
                INSERT INTO pay_by
                    (pay_by_id, pay_by_name)
                VALUES
                    (@PayById, @PayByName)";
            var parameters = new 
            { 
                PayById = payBy.PayById, 
                PayByName = payBy.PayByName 
            };
            await _dbConnection.ExecuteAsync(writeCommand, parameters);
        }

        public async Task CreatePayTypeAsync(PayType payType)
        {
            var writeCommand = @"
                INSERT INTO pay_types
                    (pay_type_id, pay_type_name)
                VALUES
                    (@PayTypeId, @PayTypeName)";
            var parameters = new 
            { 
                PayTypeId = payType.PayTypeId, 
                PayTypeName = payType.PayTypeName 
            };
            await _dbConnection.ExecuteAsync(writeCommand, parameters);
        }

        public async Task CreateRolesAsync(Roles roles)
        {
            var writeCommand = @"
                INSERT INTO roles
                    (role_name)
                VALUES
                    (@RoleName)";
            var parameters = new 
            { 
                RoleName = roles.RoleName 
            };
            await _dbConnection.ExecuteAsync(writeCommand, parameters);
        }

        public async Task CreateUnitAsync(UnitClass unitClass)
        {
            var writeCommand = @"
                INSERT INTO units
                    (unit_id, unit_name)
                VALUES
                    (@UnitId, @UnitName)";
            var parameters = new 
            { 
                UnitId = unitClass.UnitId, 
                UnitName = unitClass.UnitName 
            };
            await _dbConnection.ExecuteAsync(writeCommand, parameters);
        }

        public async Task CreateWorkerClassAsync(WorkerClass workerClass)
        {
            var writeCommand = @"
                INSERT INTO worker_classes
                    (worker_class_id, worker_class_name)
                VALUES
                    (@WorkerClassId, @WorkerClassName)";
            var parameters = new
            { 
                WorkerClassId = workerClass.WorkerClassId, 
                WorkerClassName = workerClass.WorkerClassName 
            };
            await _dbConnection.ExecuteAsync(writeCommand, parameters);
        }

        public async Task CreateWorkerTeamAsync(WorkerTeam workerTeam)
        {
            var writeCommand = @"
                INSERT INTO worker_teams
                    (worker_team_id, 
                    worker_type_id, 
                    worker_team_name, 
                    worker_contact_name, 
                    worker_contact_number, 
                    worker_contact_address, 
                    worker_account_code, 
                    worker_account_name, 
                    worker_account_number)
                VALUES
                    (@WorkerTeamId, 
                    @WorkerTypeId, 
                    @WorkerTeamName, 
                    @ContactName, 
                    @ContactNumber, 
                    @ContactAddress, 
                    @AccountCode, 
                    @AccountName, 
                    @AccountNumber)";
            var parameters = new
            {
                WorkerTeamId = workerTeam.WorkerTeamId,
                WorkerTypeId = workerTeam.WorkerTypeId,
                WorkerTeamName = workerTeam.WorkerTeamName,
                ContactName = workerTeam.ContactName,
                ContactNumber = workerTeam.ContactNumber,
                ContactAddress = workerTeam.ContactAddress,
                AccountCode = workerTeam.AccountCode,
                AccountName = workerTeam.AccountName,
                AccountNumber = workerTeam.AccountNumber
            };
            await _dbConnection.ExecuteAsync(writeCommand, parameters);
        }

        public async Task CreateWorkerTypeAsync(WorkerType workerType)
        {
            var writeCommand = @"
                INSERT INTO worker_types
                    (worker_type_id, worker_class_id, worker_type_name)
                VALUES
                    (@WorkerTypeId, @WorkerClassId, @WorkerTypeName)";
            var parameters = new 
            { 
                WorkerTypeId = workerType.WokerTypeId, 
                WorkerClassId = workerType.WorkerClassId, 
                WorkerTypeName = workerType.WorkerTypeName 
            };
            await _dbConnection.ExecuteAsync(writeCommand, parameters);
        }
    }
}
