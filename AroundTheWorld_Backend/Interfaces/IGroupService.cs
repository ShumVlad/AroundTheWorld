using AroundTheWorld_Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroundTheWorld_Backend.Interfaces
{
    public interface IGroupService
    {
        Task<bool> Add(Group group);
        Task<bool> Delete(string id);
        Group Get(string id);
        Task<bool> Update(Group group);
    }
}
