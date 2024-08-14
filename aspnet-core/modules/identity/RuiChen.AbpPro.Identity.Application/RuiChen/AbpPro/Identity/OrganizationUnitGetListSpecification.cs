using System.Linq.Expressions;
using Volo.Abp.Identity;
using Volo.Abp.Specifications;

namespace RuiChen.AbpPro.Identity
{
    public class OrganizationUnitGetListSpecification : Specification<OrganizationUnit>
    {
        private readonly OrganizationUnitGetByPagedDto input;

        public OrganizationUnitGetListSpecification(OrganizationUnitGetByPagedDto input)
        {
            this.input = input;
        }

        public override Expression<Func<OrganizationUnit, bool>> ToExpression()
        {
            Expression<Func<OrganizationUnit, bool>> expression = _ => true;

            return expression.AndIf(!input.Filter.IsNullOrWhiteSpace(),
                                     item => item.DisplayName.Contains(input.Filter) || item.Code.Contains(input.Filter));

        }

    }
}
