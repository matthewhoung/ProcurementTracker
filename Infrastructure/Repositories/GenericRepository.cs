using Dapper;
using Domain.Entities.Commons.Generic;
using Domain.Interfaces;
using System.Data;

namespace Infrastructure.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IDbConnection _dbConnection;

        public GenericRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        // Create section
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
                INSERT INTO forms_roles
                    (
                    role_name,
                    role_description
                    )
                VALUES
                    (
                    @RoleName,
                    @RoleDescription
                    )";
            var parameters = new 
            { 
                RoleName = roles.RoleName,
                RoleDescription = roles.RoleDescription
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
                WorkerTypeId = workerType.WorkerTypeId, 
                WorkerClassId = workerType.WorkerClassId, 
                WorkerTypeName = workerType.WorkerTypeName 
            };
            await _dbConnection.ExecuteAsync(writeCommand, parameters);
        }
        // Read section

        public async Task<List<WorkerClass>> GetAllWorkerClassesAsync()
        {
            var readCommand = @"
                SELECT 
                    worker_class_id AS WorkerClassId,
                    worker_class_name AS WorkerClassName
                FROM 
                    worker_classes";
            var workerClasses = await _dbConnection.QueryAsync<WorkerClass>(readCommand);
            return workerClasses.AsList();
        }

        public async Task<List<WorkerType>> GetAllWorkerTypesAsync()
        {
            var readCommand = @"
                SELECT 
                    worker_type_id AS WorkerTypeId,
                    worker_class_id AS WorkerClassId,
                    worker_type_name AS WorkerTypeName
                FROM 
                    worker_types";
            var workerTypes = await _dbConnection.QueryAsync<WorkerType>(readCommand);
            return workerTypes.AsList();
        }

        public async Task<List<WorkerTeam>> GetAllWorkerTeamsAsync()
        {
            var readCommand = @"
                SELECT 
                    worker_team_id AS WorkerTeamId,
                    worker_type_id AS WorkerTypeId,
                    worker_team_name AS WorkerTeamName,
                    worker_contact_name AS ContactName,
                    worker_contact_number AS ContactNumber,
                    worker_contact_address AS ContactAddress,
                    worker_account_code AS AccountCode,
                    worker_account_name AS AccountName,
                    worker_account_number AS AccountNumber
                FROM 
                    worker_teams";
            var workerTeams = await _dbConnection.QueryAsync<WorkerTeam>(readCommand);
            return workerTeams.AsList();
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            var readCommand = @"
                SELECT 
                    department_id AS DepartmentId,
                    department_name AS DepartmentName
                FROM 
                    departments";
            var departments = await _dbConnection.QueryAsync<Department>(readCommand);
            return departments.AsList();
        }

        public async Task<List<PayBy>> GetAllPayBysAsync()
        {
            var readCommand = @"
                SELECT 
                    pay_by_id AS PayById,
                    pay_by_name AS PayByName
                FROM
                    pay_by";
            var payBys = await _dbConnection.QueryAsync<PayBy>(readCommand);
            return payBys.AsList();
        }

        public async Task<List<PayType>> GetAllPayTypesAsync()
        {
            var readCommand = @"
                SELECT 
                    pay_type_id AS PayTypeId,
                    pay_type_name AS PayTypeName
                FROM
                    pay_types";
            var payTypes = await _dbConnection.QueryAsync<PayType>(readCommand);
            return payTypes.AsList();
        }

        public async Task<List<Roles>> GetAllRolesAsync()
        {
            var readCommand = @"
                SELECT
                    role_id AS RoleId,
                    role_name AS RoleName,
                    role_description AS RoleDescription
                FROM
                    forms_roles";
            var roles = await _dbConnection.QueryAsync<Roles>(readCommand);
            return roles.AsList();
        }

        public async Task<List<UnitClass>> GetAllUnitsAsync()
        {
            var readCommand = @"
                SELECT 
                    unit_id AS UnitId,
                    unit_name AS UnitName
                FROM
                    units";
            var units = await _dbConnection.QueryAsync<UnitClass>(readCommand);
            return units.AsList();
        }
    }
}
