using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ApprovalStatuses.Queries.GetAll;
using Application.Areas.Queries.GetAll;
using Application.Dtos;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class ApprovalStatusService : IApprovalStatusService
    {

        private readonly IQueryHandler<ApprovalStatusGetAllQry,List<ApprovalStatusDto>> _approvalStatusGetAllHandler;

        public ApprovalStatusService(IQueryHandler<ApprovalStatusGetAllQry, List<ApprovalStatusDto>> approvalStatusGetAllHandler)
        {
            _approvalStatusGetAllHandler = approvalStatusGetAllHandler;
        }

        public async Task<List<GenericResponse>> GetAll()
        {
            var results = new List<GenericResponse>();
            var statuses = await _approvalStatusGetAllHandler.Handle(new ApprovalStatusGetAllQry());
            foreach (var status in statuses)
            {
                var tempStatus = new GenericResponse() { id = status.Id.Value, name = status.Name };
                results.Add(tempStatus);
            }
            
            return results;
        }
    }
}
