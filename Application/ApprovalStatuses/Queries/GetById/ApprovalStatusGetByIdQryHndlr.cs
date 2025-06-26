using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Areas.Queries.GetAll;
using Application.Dtos;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApprovalStatuses.Queries.GetById
{
    public class ApprovalStatusGetByIdQryHndlr : IQueryHandler<ApprovalStatusGetByIdQry, Domain.Entities.ApprovalStatus?>
    {
        private readonly IApprovalStatusRepository _approvalStatusRepository;

        public ApprovalStatusGetByIdQryHndlr(IApprovalStatusRepository approvalStatusRepository)
        {
            _approvalStatusRepository = approvalStatusRepository;
        }

        public async Task<Domain.Entities.ApprovalStatus?> Handle(ApprovalStatusGetByIdQry query)
        {
            if (query.ApprovalStatusDto.Id == null) { return null; }

            var id = query.ApprovalStatusDto.Id.Value;
            var area = await _approvalStatusRepository.GetById(id);

            if (area == null) { return null; }
            return area;
        }
    }
}
