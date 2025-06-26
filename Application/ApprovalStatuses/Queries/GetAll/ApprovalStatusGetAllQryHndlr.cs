using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.ApprovalStatuses.Queries.GetAll
{
    public class ApprovalStatusGetAllQryHndlr : IQueryHandler<ApprovalStatusGetAllQry,List<ApprovalStatusDto>>
    {
        private readonly IApprovalStatusRepository _approvalStatusRepository;

        public ApprovalStatusGetAllQryHndlr(IApprovalStatusRepository approvalStatusRepository)
        {
            _approvalStatusRepository = approvalStatusRepository;
        }

        public async Task<List<ApprovalStatusDto>> Handle(ApprovalStatusGetAllQry query)
        {
            var statuses = await _approvalStatusRepository.GetAll(); 

            var dtoApprovalStatuses = new List<Application.Dtos.ApprovalStatusDto>();


            foreach (var status in statuses)
            {
                var temp = new Application.Dtos.ApprovalStatusDto()
                {
                    Name = status.Name,
                    Id = status.Id,
                };
                dtoApprovalStatuses.Add(temp);
            }

            return dtoApprovalStatuses;
        }
    }
}
