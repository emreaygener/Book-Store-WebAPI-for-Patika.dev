using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using static WebApi.Common.ViewModels;

namespace WebApi.Applications.UserOperations.Queries.GetUsers
{
    public class GetUsersQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<UsersViewModel> Handle()
        {
            var usersList = _context.Users.OrderBy(x => x.Id).ToList();

            return _mapper.Map<List<UsersViewModel>>(usersList);
        }
    }
}