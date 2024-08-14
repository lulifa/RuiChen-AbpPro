using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Specifications;
using VoloIOrganizationUnitRepository = Volo.Abp.Identity.IOrganizationUnitRepository;

namespace RuiChen.AbpPro.Identity
{
    public interface IOrganizationUnitRepository : VoloIOrganizationUnitRepository
    {
        Task<int> GetCountAsync(ISpecification<OrganizationUnit> specification, CancellationToken cancellationToken = default);

        Task<List<OrganizationUnit>> GetListAsync(
            ISpecification<OrganizationUnit> specification,
            string sorting = nameof(OrganizationUnit.Code),
            int maxResultCount = 10,
            int skipCount = 0,
            bool includeDetails = false,
            CancellationToken cancellationToken = default);
    }
}
