using System.Collections.Generic;
using System.Threading.Tasks;
using TCUEMS_BackendNew.Models;

namespace TCUEMS_BackendNew.Data
{
    public interface ISemesterWarningRepository
    {
        Task<IEnumerable<SemesterWarning>> GetAllSemesterWarnings();
        Task<IEnumerable<SemesterWarning>> GetSemesterWarningsByCriteria(SemesterWarning criteria);
    }
}
