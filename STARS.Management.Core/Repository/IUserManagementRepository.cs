using System.Collections.Generic;
using System.Threading.Tasks;
using STARS.Management.Core.DTO;

namespace STARS.Management.Core.Repository;

    public interface IUserManagementRepository
    {
        public Task<List<UserDTO>> GetAllUsers();

    }
