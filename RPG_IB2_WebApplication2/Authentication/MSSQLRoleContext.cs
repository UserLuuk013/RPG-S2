using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Authentication
{
    public class MssqlRoleContext : IRoleStore<Role>
    {
        private readonly string _connectionString;
        public MssqlRoleContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //nothing to do.
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Rol", connection);
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        Role role = default(Role);
                        if (sqlDataReader.Read())
                        {
                            role = new Role();
                        }
                        return Task.FromResult<Role>(role);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
